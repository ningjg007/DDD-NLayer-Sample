$(document).ready(function () {
    $('.ajax-remove').click(function () {
        $('#confirm_modal').modal();
        var url = $(this).attr('href');
        $('#confirm_title').html('删除确认');
        var name = $(this).attr('data-name');
        $('#confirm_content').html('确认删除【' + name + '】吗？');
        $('#confirm_confirm').one('click',function () {
            $.ajax({
                type: "GET",
                url: url,
                dataType: "json",
                success: ajaxRequestSuccess
            });
            $('#confirm_modal').modal('hide');
            return false;
        });
        return false;
    });

    var url = location.href;
    $(".submenu").each(function() {
        var menu = $(this).data("menu");
        if (url.match("/" + menu)) {
            $(this).addClass("open");
        }
    });
    
    $("#sidebar .open li a").each(function () {
        if (url.match($(this).attr("href"))) {
            $(this).parent().addClass("active");
        }
    });

    $(".chkAll").click(function () {
        var checked = this.checked;
        var depth = $(this).attr("depth");
        var node = $(this);
        while (depth>0) {
            node = node.parent();
            depth--;
        }
        $("input[type=checkbox]", node).each(function() {
            $(this).attr("checked", checked);
        });
    });
});

function ajaxRequestSuccess(resp) {
    if (!!resp) {
        if (resp.Succeeded) {
            var url = null;
            var msg = "Succeed!!!";
            if (!!resp.RedirectUrl && resp.RedirectUrl.trim().length > 0) {
                url = resp.RedirectUrl;
            }
            if (resp.ShowMessage) {
                if (!/^\s*$/.test(resp.Message) && resp.Message != null) {
                    msg = resp.Message;
                }
            } else {
                msg = null;
            }
            if (!!url) {
                window.location.href = url;
            }
            if (!!msg) {
                $.gritter.add({
                    title: '提示',
                    text: msg,
                    sticky: false
                });
            }
        } else {
            $.gritter.add({
                title: '提示',
                text: resp.ErrorMessage,
                sticky: false
            });
        }
    }
}
