$(function () {
    $(window).load(function () {
        getProducts(1);
    });
    $("#kind").change(function () {
        getProducts(1);
    })
    $("#order").change(function () {
        getProducts(1);
    })
    $("#range").change(function () {
        getProducts(1);
    })
    function pages(total,active) {
        var text = '<ul class="pagination" style="display:block" >';
        for (i = 1; i <= total; i++) {
            if (i == active) {
                text += '<li class="active"><a>' + i + '</a></li>';
            }
            else text += '<li><a>' + i + '</a></li>';
        }
        text += '</ul>';
        return text;
    }
    function getProducts(pagenumber) {
        var index = $("#kind").children("option:selected").index();
        var cat;
        if (index == 0) {
            cat = "all";
        }
        if (index == 1) {
            cat = "pants";
        }
        if (index == 2) {
            cat = "shirts";
        }
        var order = $("#order").children("option:selected").index();
        var price = $("#range").children("option:selected").attr("data-id");
        $.ajax({
            url: "/Shop/ViewProduct",
            type: "get",
            data: { cat: cat, page: pagenumber, order: order,price:price },
            beforeSend: function () {
                $("#replace").text("Đang load sản phẩm...");
            },
            success: function (data) {
                $("#replace").html(data);
                $("#pagination").html(pages($("#pages").attr("data-id"), pagenumber));
                $(".pagination>li>a").on("click", function () {
                    var index = parseInt($(this).text());
                    getProducts(index);
                })
            }
        })
    }
})