
$(document).ready(function () {

    $(document).on('click', '.add_comment', function (e) {
        //.$('.comment-input');
        console.log(1);
        const input = $(this).parent().siblings().children();
        const commenterId = $(this).data('commenter-id');
        console.log(input);
        const comments = $(this).parent().parent().siblings('.row').next().children().children();
        const postId = $(this).parent().parent().parent().attr('id');
        let comment = input.val();
        const commentObject = {
            Content: comment,
            PersonId: commenterId,
            PostId: postId

        };

        $.ajax({
            type: "POST",
            url: "/USER/AddComment",
            data: commentObject,
            success: function (comment) {
                date = moment(comment.DateCommented).format("M/D/Y h:mm:ss a");

                console.log(comment);
                let newcomment = ` <li class="comment" id="${comment.Id}">
                            <div class="row comment-header">
                                <div class="col-sm-8">
                                    <h5 class="comment-onwer" id="${comment.Person.Id}">${comment.Person.FirstName} ${comment.Person.LastName === null ? '' : comment.Person.LastName}</h5>
                                    <span class="comment-date">${date}</span>
                                </div>
                                <div class="col-sm-4">

                                    <a href="/User/Delete/${comment.Id}" class="link comment-link">Delete</a>

                                    <a href="/User/Edit/${comment.Id}" class="link comment-link">Edit</a>
                                </div>
                            </div>
                            <div class="row comment-content">
                                <div class="col-xs-12">
                                    <span class="" id="content">
                                        ${comment.Content}
                                    </span>

                                </div>
                            </div>
                        </li>`;
                comments.append(newcomment);
                input.val('');
            }

        });


        e.preventDefault();
    });

    $(".add_post").on('click',function (e) {

        const postContent = $('.post-input').val();
        const btn = $(this);
        const posterId = btn.data('profile-id');
        const target = btn.data('place-id');
        const eventid = btn.data('event-id');

        if (postContent !== '') {
          

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
                       
                            <a href="/User/Delete/${post.Id}" class="link post-link">Delete</a>

                            <a href="/User/Edit/${post.Id}" class="link post-link">Edit</a>
                   
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
                    <button class="btn btn-default form-control">Comment</button>
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


});
