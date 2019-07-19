
$(document).ready(function () {

    // adding Comment to a post
    $('.posts').on('click', '.add_comment', function (e) {
        //getting The Comment input field
        const input = $(this).parents('.comment-holder').find("input.comment-input");
        //getting the comment text
        let comment = input.val();

        //getting commenter id
        const commenterId = $(this).data('commenter-id');
        //getting The Comments block
        const comments = $(this).parents('div.post').find('ul.comments');

        //getting post Id
        const postId = $(this).parents('.post').attr('id');

        //Make The Comment Object To HttppostIt
        const commentObject = {
            Content: comment,
            PersonId: commenterId,
            PostId: postId

        };
        //Making The Ajax Call To The Comment To THe Db 
        $.ajax({
            type: "POST",
            url: "/USER/AddComment",
            data: commentObject,
            success: function (comment) {

                //getting TheResponse as Object And Structuring It To The Comments Block
                date = moment(comment.DateCommented).format("M/D/Y h:mm:ss a");

                let newcomment = ` <li class="comment" id="${comment.Id}">
                            <div class="row comment-header">
                                <div class="col-sm-8">
                                    <h5 class="comment-onwer" id="${comment.Person.Id}">${comment.Person.FirstName} ${comment.Person.LastName === null ? '' : comment.Person.LastName}</h5>
                                    <span class="comment-date">${date}</span>
                                </div>
                                <div class="col-sm-4">
                                <a href="#" id="${comment.Id}" class="link comment-link comment-delete  delete-link">Delete</a>

                                <a href="#" id="${comment.Id}" class="link comment-link comment-edit edit-link">Edit</a>
                                </div>
                            </div>
                            <div class="row comment-content">
                                <div class="col-xs-12">
                                    <span class="" id="content">${comment.Content}</span>

                                </div>
                            </div>
                        </li>`;

                //append The Comment To The Block
                comments.append(newcomment);
                //clearing The Input Field
                input.val('');
            }

        });


        e.preventDefault();
    });

    //Adding Post To The Posts Section Home Or Event
    $(".add_post").on('click',function (e) {

        const postContent = $('.post-input').val();
        const btn = $(this);
        const posterId = btn.data('profile-id');
        const target = btn.data('place-id');
        const eventid = btn.data('event-id');
        $('.post-input').val('');
        if (postContent !== '') {
          
            //check if Target is Home Or Event
            if (target == 2) {
                var Post = {
                    Content: postContent,
                    PersonId: posterId,
                    TypeId: target,
                    EventId: eventid
                }
                var url = '/Events/AddPost';
            } else {

                var url = '/User/AddPost';
                var Post = {
                    Content: postContent,
                    PersonId: posterId,
                    TypeId: target
                }
            }
            console.log(Post);

            $.ajax({
                type: 'POST',
                url: url,
                data: Post,
                success: function (post) {
                    console.log(post);
                    console.log(typeof post.DatePosted);
                    //var date = new Date(parseInt(post.DatePosted.substr(6)));
                    //date.format("m/d/y h:MM:ss TT")
                    //var date = new Date(post.DatePosted);
                    date = moment(post.DatePosted).format("M/D/Y h:mm:ss a");
                    //console.log(date);


                    let newPost = `<div class="post" id="${post.Id}" >
            <div class="post-body">
                <div class="row post-header">
                    <div class="col-xs-8">
                        <h4 class="post-owner"  id="${post.Person.Id}">${post.Person.FirstName} ${post.Person.LastName}</h4>
                        <span class="post-date">${date}</span>
                    </div>
                    <div class="">
                       
                             <button class="link post-delete post-link btn btn-link">Delete</button>

                            <buttton class="link post-edit post-link btn btn-link">Edit</buttton>
                   
                    </div>
                </div>
                <div class="row post-content">
                    <div class="col-sm-12">
                        <span class="">${post.Content}</span>
                    </div>
                </div>
            </div>
            <div class="row post-interact">
                <div class="col-xs-4 form-group">
                    <button class="btn btn-default form-control">Like</button>
                </div>
                <div class="col-xs-4 form-group">
                    <button class="btn btn-default form-control" id="showComments">Comment</button>
                </div>
                <div class="col-xs-4 form-group">
                    <button class="btn btn-default form-control">Share</button>
                </div>
            </div>
            <div class="row ">
                <div class="col-sm-12 post-comments">
                    <ul class="comments">

                    </ul>
                </div>
            </div>
            <div class="row comment-holder">
                <div class="col-xs-8  col-sm-10 form-group">
                    <input type="text" placeholder="Add Comment" class="comment-input form-control" />
                </div>
                <div class="col-xs-4 col-sm-2 form-group">
                    <button class="btn btn-primary add_comment form-control" id="comment_id" data-commenter-id="${post.Person.Id}">
                        Comment
                    </button>
                </div>
            </div>


        </div>`;
                    $('section.posts').prepend(newPost);

                }

            });
        }
        e.preventDefault();

    });


    //Delete Post 
    // showing Delete Confirmation message and Delete Post
    $('.posts').on("click", ".post-delete", function (e) {

        var postParent = $(this).parents('div.post');
        //Two options To Get the id of the Post i want to Delete
        var myPostId = $(this).parents('div.post').attr('id');
        //var myPostId = $(this).attr('id');

        //Change Modal
        $(".modal-footer #selectedId").val(myPostId);
        $(".modal-header").css({ "border-bottom": " solid red 1px", "background": "rgb(227, 227, 227,.7)" });
        $(".modal-title").css({ "font-weight": "bold" }).text("Delete");
        $('.modal-body').text("Are You Sure You Want To Delete This Post");

        //Showing Confirmation Message Modal
        $("#exampleModal").modal("show");

        $('.modal').on("click", ".delbtn", function () {
           
            //ajax call To Remove The Comment From DataBase
            
            $.ajax({
                type: "POST",
                url: "/User/DeletePost/"+ myPostId,
                success: function () {
                    postParent.remove();
                },
                error: function () {
                    console.log("Error Deleting Post");
                }

            });
           
            $("#exampleModal").modal("hide");
        });
        e.preventDefault;
    });



    //EditPost in Place
    $('.posts').on('click', '.post-edit', function (e) {
        var postparent = $(this).parents('div.post');
        var links = $(this).parent().find(".link");
        links.css('display', "none");
        let postContent = postparent.find(".post-content #content");
        let postText = postContent.text();
        console.log(postText);
        let input = `<div class="input-holder " style="background:transparent">
    <textarea id="input" class="form-control post-input" cols='20' value='${postText}' >${postText}</textarea>
    <button id="edit" class="btn btn-primary m-auto">Confirm</button></div>`;
        postContent.replaceWith(input);

        $('.post').on('click', "#edit", function (e) {

            //selecting The Div of input holder To Easily Delete
            var editedparent = $(this).parents();
            var selcteddiv = editedparent.children(".input-holder");
            var postId = $(this).parents('div.post').attr('id');
            var input = selcteddiv.children('#input');
            let inputval = input.val();
            if (inputval != '') {
                let newPost = `<span id="content">${inputval}</span>`;
                $(selcteddiv).replaceWith(newPost);
                links.css('display', "inline-block");
                //ajax Call To Edit The Post in The Db  
               

                //Make The Ajax Call To Post The Edited Content of Post
                $.ajax({
                    type: "POST",
                    url: "/User/EditPost",
                    data: { id:postId, val:inputval },
                    success: function () {
                        console.log("Hey");
                    }
                });

            } else {
                input.css('border-color', 'red');
            }
            e.preventDefaults;
        });
        e.preventDefaults;
    });


    //Show and Hide Comments
    $('.post').on('click', '#showComments', function (e) {

        var clickedBtn = $(this);
        var PostParent = clickedBtn.parents('div.post');
        var postComments = PostParent.find("div.post-comments");
        postComments.slideToggle();

    });



    //Edit Comment in Place
    $('.post').on('click', ".comment-edit", function (e) {
        var commentparent = $(this).parents('li');
        var links = $(this).parent().find(".link");

        links.css('display', "none");
        let comment = commentparent.find("#content");
        let commentText = comment.text();
        let input = `<div class="input-holder row form-group" style="background:transparent">
    <input id="input" class="form-control comment-input col-md-8" type='text' value='${commentText}' >
    <button id="edit" class="btn btn-primary col-md-2 edit_comment m-auto">Confirm</button></div>`;
        comment.replaceWith(input);
        
        $('.comments').on('click', "#edit", function (e) {

            //selecting The Div of input holder To Easily Delete

            var editedparent = $(this).parents();
            var selcteddiv = editedparent.children(".input-holder");
            var commentId = $(this).parents('li.comment').attr('id');
            var input = selcteddiv.children('#input');
            let inputval = input.val();
            if (inputval != '') {
                let newComment = `<span class="" id="content">${inputval} </span>`;
                $(selcteddiv).replaceWith(newComment);
                links.css('display', "inline-block");
                //ajax Call To Edit The Comment in The Db  
                //Make The Ajax Call To Post The Edited Content of Post
                $.ajax({
                    type: "POST",
                    url: "/User/EditComment",
                    data: { id: commentId, val: inputval },
                    success: function () {
                        console.log("Hey");
                    }
                });

            } else {
                input.css('border-color', 'red');
            }
            e.preventDefaults;
        });
        e.preventDefaults;
    });

    // showing Delete Confirmation message and Delete Comment
    $('.posts').on("click", ".comment-delete", function () {

        var commentparent = $(this).parents('li.comment');
        //Two options To Get the id of the Comment i want to Delete
        var commentId = $(this).parents('li.comment').attr('id');
        var myCommentId = $(this).attr('id');

        //Change Modal
        $(".modal-footer #selectedId").val(myCommentId);
        $(".modal-header").css({ "border-bottom": " solid red 1px", "background": "rgb(227, 227, 227,.7)" });
        $(".modal-title").css({ "font-weight": "bold" }).text("Delete");
        $('.modal-body').text("Are You Sure You Want To Delete This Comment");

        //Showing Confirmation Message Modal
        $("#exampleModal").modal("show");

        $('.modal').on("click", ".delbtn", function () {
            //ajax call To Remove The Cimment From DataBase
            $.ajax({
                type: "POST",
                url: "/User/DeleteComment/" + myCommentId,
                success: function () {
                    commentparent.remove();

                },
                error: function () {
                    console.log("Error Deleting Comment");
                }

            });

            $("#exampleModal").modal("hide");
        });
    });

});
