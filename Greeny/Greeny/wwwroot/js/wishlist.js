$(document).ready(function () {


    //add wishlist
    $(document).on("submit", "#wishlist-form", function (e) {
        e.preventDefault();
        let productId = $(this).attr("data-id");
        let data = { id: productId };

        $.ajax({
            url: "wishlist/addbasket",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup.wishlist-count").text(res)
            }
        })

    })

    //delete from basket
    $(document).on("submit", "#basket-delete-form", function (e) {
        e.preventDefault();       
        let productId = $(this).attr("data-id");

        $(this).parent().parent().remove();

        let data = { id: productId };

        $.ajax({
            url: "wishlist/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup.wishlist-count").text(res.count);
                if (res.count != 0) {
                    $("#total-price").text(res.total);
                } else {
                    $("#total-price").addClass("d-none");
                }

            }
        })
    })




})