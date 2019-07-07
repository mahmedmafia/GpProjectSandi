$(document).ready(function () {
    $('.joinbtn').on("click", function (e) {
        e.preventDefault();
        var btn = $(this);
        var eventid = btn.attr("id");
        var btnid = '#' + eventid;
        var val = btn.text();
        $.ajax({
            type: "Post",
            url: "/Events/JoinEvent/" + eventid,
            success: function () {
                if (val === 'UnJoin') {
                    btn.text("Join");
                } else {
                    btn.text("UnJoin");
                }
                //if (btn.data("target") != null) {

                var tab = $('[data-tab-content="tab3"]').data("tab-content");
                var tabcontent = $(".tab-content[data-tab-content=" + tab + "]");
                var listitem = $("li[data-tab-content=" + tab + "]");
                if (listitem.hasClass("active")) {
                    var EventId = listitem.attr("id");
                    console.log(tab + tabcontent + EventId);
                    if (EventId != null) {
                        callusers(EventId, tabcontent);
                    }
                }
                //}
            },
            error: function () {
                console.log("Error");
            }
        });

    });
    var callusers = function (EventId, element) {
        var url = "/Events/JoinedUsers?Id=" + EventId;
        $.ajax({
            type: "Post",
            url: url,
            success: function (response) {

                if (response == null) {
                    response =
                        "<div><p>Empty Bitch</p></div>";
                    console.log(response);
                    element.innerWidth = response;
                }
                element.html(response);

            },
            error: function () {
                console.log("Error");
            }
        });
    }

    $("#tabs li").click(function () {
        $("#tabs .active").removeClass("active");
        var clicked = $(this);
        var tab = $(this).addClass('active').data('tab-content');
        $(".tab-content.active").removeClass("active");
        $(".tab-content[data-tab-content=" + tab + "]").addClass("active");
        var tabcontent = $(".tab-content[data-tab-content=" + tab + "]");
        var EventId = $(this).attr("id");
        if (EventId != null) {
            callusers(EventId, tabcontent);
        }
    });

    $("[type = submit]").click(function () {
        var post = $("textarea").val();
        $("<p class='post'>" + post + "</p>").appendTo("section");
    });

 


});