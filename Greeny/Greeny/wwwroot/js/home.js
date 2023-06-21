$(document).ready(function () {
       
    $(document).on("click", ".show-more-btn", function () {

        let parent = $("#parent");
        let skipCount = $(parent).children().length;     
        let productCount = $("#product").attr("data-count");
       
        $.ajax({
            url: `home/showmoreorless?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
               $(parent).append(res);
                let skipCount = $(parent).children().length;
                if (skipCount >= productCount) {
                    $(".show-more-btn").addClass("d-none")
                    $(".show-less-btn").removeClass("d-none")
                }
            }
        })
    })


    $(document).on("click", ".show-less-btn", function () {
        let parent = $("#parent");
        let skipCount = 0;

        $.ajax({
            url: `home/showmoreorless?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(parent).empty();
                $(parent).append(res);
                $(".show-less-btn").addClass("d-none")
                $(".show-more-btn").removeClass("d-none")

            }
        })
    })


    $(document).on("click", ".more-btn", function () {

       
        let parent = $("#parent-elem");
        let skipCount = $(parent).children().length;
        let productCount = $("#product-elem").attr("data-count");

        $.ajax({
            url: `home/moreorless?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(parent).append(res);
                let skipCount = $(parent).children().length;
                if (skipCount >= productCount) {
                    $(".more-btn").addClass("d-none")
                    $(".less-btn").removeClass("d-none")
                }
            }
        })
    })

    $(document).on("click", ".less-btn", function () {
        let parent = $("#parent-elem");
        let skipCount = 0;

        $.ajax({
            url: `home/moreorless?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(parent).empty();
                $(parent).append(res);
                $(".less-btn").addClass("d-none")
                $(".more-btn").removeClass("d-none")

            }
        })
    })




 





})