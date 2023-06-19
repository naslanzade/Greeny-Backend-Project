$(document).ready(function () {
      

    $(document).on("click", ".status-slider", function () {

        let sliderId = $(this).attr("data-id");       
        let data = { id: sliderId };
        let changeElem = $(this);

        $.ajax({
            url: "slider/changestatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    $(changeElem).removeClass("status-false");
                    $(changeElem).addClass("status-true");
                } else {
                    $(changeElem).removeClass("status-true");
                    $(changeElem).addClass("status-false");
                }
               

            }
        })
    })

})