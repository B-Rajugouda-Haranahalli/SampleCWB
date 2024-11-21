var OperationListUtil = {
    FilterOperationList: () => {
        var searchObject = {};
        $(".operation-list-search").each(function () {
            var val = $.trim($(this).val())
            if (val.length != 0) {
                searchObject[$(this).data("key")] = val.toUpperCase();
            }
        });
        AppUtil.TableFilter("tbl-operation-list", searchObject);
    }
}
$(function () {
    $(".operation-list-search").change(function () {
        OperationListUtil.FilterOperationList();
    });
});