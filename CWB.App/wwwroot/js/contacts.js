var ContactsUtil = {
    FilterContacts: () => {
        var searchObject = {};
        $(".contact-search").each(function () {
            var val = $.trim($(this).val())
            if (val.length != 0) {
                searchObject[$(this).data("key")] = val.toUpperCase();
            }
        });
        AppUtil.TableFilter("tbl-contacts", searchObject);
    }
}
$(function () {
    $(".contact-search").change(function () {
        ContactsUtil.FilterContacts();
    });
});