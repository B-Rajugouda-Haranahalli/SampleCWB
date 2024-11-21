
function loadDocList() {
    api.getbulk("/DocumentManagement/GetAllDocList").then((data) => {
        //data = data.filter(item => item.status !== 6);
        var tablebody = $("#DocViewListGrid tbody");
        $(tablebody).html("");//empty tbody
        console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("DocListGridRow", data[i]));
            //console.log($(tablebody).append(AppUtil.ProcessTemplateData("DocListGridRow", data[i])));
        }
    }).catch((error) => {
        console.log(error);
    });
}
function loadDocCategory() {
    var selElem = $('#SearchDocCat');
    selElem.html('');
    api.getbulk("/DocumentManagement/GetAllDocCategory").then((data) => {

        div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        selElem.append(div_data);
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].docCategoryId + "'>" + data[i].docCategoryDesc + "</option>";
            selElem.append(div_data);
        }
    });
}

$(document).ready(function () {
    $('#select-all-checkbox').change(function () {
        if ($(this).is(":checked")) {
            $('#DocViewListGrid tbody').find('input[type="checkbox"]').prop('checked', true);
            $('#btnDeleteFiles').prop('disabled', false);
        } else {
            $('#DocViewListGrid tbody').find('input[type="checkbox"]').prop('checked', false);
            $('#btnDeleteFiles').prop('disabled', true);
        }
    });

    function handleCheckboxChange() {
        var checkboxes = $("#DocViewListGrid tbody input[type='checkbox']:checked"); // Select checked checkboxes
        // Determine the button state based on the number of selected checkboxes and unique part IDs
        if (checkboxes.length >= 1) {
            $('#btnDeleteFiles').prop('disabled', false); // Enable the btnAG button
        }
        else {
            $('#btnDeleteFiles').prop('disabled', true);
        }
    }

    // Attach the handleCheckboxChange function to the change event of the checkboxes using event delegation
    $('#DocViewListGrid tbody').on('change', 'input[type="checkbox"]', handleCheckboxChange);

    // Optionally, trigger the event handler once to set the initial state of the button
    handleCheckboxChange();

    loadDocList();
    loadDocCategory();
    $('#SearchDocCat').select2();

    $("#SearchDocCat").on("change", function () {
        var selectedValue = $(this).val().toLowerCase();
        if (selectedValue == "0") {
            $("#DocViewListGrid tbody tr").show();
            return;
        }
        $("#DocViewListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[11]).text().toLowerCase().indexOf(selectedValue) > -1)
        });// show only the filtered rows
    });

    $('#SearchDataShareCust').on('click', function () {
        if ($(this).is(':checked')) {
            var v = 'Y';
            var value = v.toLowerCase();
            $("#DocViewListGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#DocViewListGrid tbody tr").show(); // show all rows when checkbox is unchecked
        }
    });
    $('#SearchArchive').on('click', function () {
        if ($(this).is(':checked')) {
            var v = 'Y';
            var value = v.toLowerCase();
            $("#DocViewListGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#DocViewListGrid tbody tr").show(); // show all rows when checkbox is unchecked
        }
    });

    $("#ClearSearchFields").on("click", function () {
        $("#DocViewListGrid tbody tr").show();
        $("#SearchDocTypeName").val('');
        $("#SearchPartNo").val('');
        $("#SearchRoutingName").val('');
        $("#SearchOrpNo").val('');
        $("#SearchCustomer").val('');
        $("#SearchWo").val('');
        $("#SearchUploadFrom").val('');
        $("#SearchUploadTo").val('');
        $("#SearchWo").val('');
        $("#SearchDataShareCust").prop('checked', false);
        $("#SearchArchive").prop('checked', false);
        var SearchDocCat = $('#SearchDocCat');
        SearchDocCat.val(0).trigger('change');
    });
    $("#SearchDocTypeName").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocViewListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#SearchPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocViewListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#SearchRoutingName").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocViewListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#SearchOrpNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocViewListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#SearchCustomer").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocViewListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $('#RetentionDate').on('shown.bs.modal', function (event) {
        //SelectDocType();
        var relatedTarget = $(event.relatedTarget);
        var doclistid = relatedTarget.data("doclistid");
        var retdate = relatedTarget.data("retdate");
        $("#P5CurrentDate").val(retdate);
        $("#P5DocListId").val(doclistid);
    });
    //RetentionSave
    $("#RetentionSave").on("click", function () {
        var P5DocListId = parseInt($("#P5DocListId").val());
        var P5RetentionDate = new Date(Date.parse($("#P5RetentionDate").val()));
        const currentDate = $('#P5CurrentDate').val();
        const [month, day, year] = currentDate.split('-'); // Destructuring assignment
        const rstDt = new Date(year, month - 1, day); // Month is 0-indexed
        const restrictDt = new Date(rstDt);
        var formattedDate;
        if (isNaN(P5DocListId)) {
            P5DocListId = 0;
        }
        if (isNaN(P5RetentionDate.getTime())) {
            var newNamevalidate = document.getElementById('P5RetentionDate');
            newNamevalidate.style.border = '2px solid red';
            return false;

            // or display an error message to the user
        } else if (P5RetentionDate <= restrictDt) {
            var newNamevalidate = document.getElementById('P5RetentionDate');
            newNamevalidate.style.border = '2px solid red';
            alert("New Date Retained Should Be Greater Than Current Date Retained");
            return false;
            // or display an error message to the user
        }
        else {
            formattedDate = P5RetentionDate.toISOString();
        }
        //if (isNaN(CDRPRetPerYear)) {
        //    var newNamevalidate = document.getElementById('CDRPRetPerYear');
        //    newNamevalidate.style.border = '2px solid red';
        //    return false;
        //}
        var rowData = {
            docListId: P5DocListId,
            deletionDate: formattedDate
        };
        $.ajax({
            type: "POST",
            url: '/DocumentManagement/PostDocList',
            contentType: "application/json; charset=utf-8",
            headers: { 'Content-Type': 'application/json' },
            data: JSON.stringify(rowData),
            dataType: "json",
            success: function (result) {
                //var CustRetnDataId = result.custRetnDataId;
                //$("#CDRPCustRetId").val(CustRetnDataId);
                var newNamevalidate = document.getElementById('P5RetentionDate');
                newNamevalidate.style.border = '';
                loadDocList();
                $('#RetentionDate').modal('hide');
            }
        });
    });

    $("#btnDeleteFiles").on("click", function () {
        var checkboxes = $("#DocViewListGrid tbody input[type='checkbox']:checked");
        var selectedRowsData = {};
        var docIdList = {};
        checkboxes.each(function (index, checkbox) {
            var row = checkbox.parentNode.parentNode;
            var rowData = {
                docListId: parseInt($(row).find("td:eq(13)").text()),
                fileName: $(row).find("td:eq(15)").text(),
                storageLocation: $(row).find("td:eq(14)").text()
            };
            // Group by PartId
            docIdList[rowData.docListId] = rowData;
                
        });

        selectedRowsData = Object.values(docIdList);
        let confirmval = confirm("Are your sure you want to delete this ?", "Yes", "No");
        if (confirmval) {
            if (selectedRowsData.length >= 1) {
                $.ajax({
                    type: "POST",
                    url: '/DocumentManagement/DeleteDocListAndFile',
                    contentType: "application/json; charset=utf-8",
                    headers: { 'Content-Type': 'application/json' },
                    data: JSON.stringify(selectedRowsData),
                    dataType: "json",
                    success: function (result) {
                        console.log(result);
                        if (result) {
                            loadDocList();
                        }
                    }
                });
            }
        }
    });
});


function viewFile(element) {
    var relatedTarget = $(element);
    var file = relatedTarget.data("filename");
    var doctypename = relatedTarget.data("doctypename");
    var customername = relatedTarget.data("customername");
    var partno = relatedTarget.data("partno");
    var partdesc = relatedTarget.data("partdesc");
    var routingname = relatedTarget.data("routingname");
    var oprno = relatedTarget.data("oprno");
    var retdate = relatedTarget.data("retdate");
    $('#viewDoc').modal('show');
    $("#DocTypenameText").text(doctypename);
    $("#PartNoText").text(partno);
    $("#PartDescText").text(partdesc);
    $("#CustomerText").text(customername);
    $("#RetentionDateText").text(retdate);
    $("#RoutingNameText").text(routingname);
    $("#OprNoText").text(oprno);


    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/masters/ViewFile?fileName=' + file, true);
    xhr.responseType = 'arraybuffer';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: "application/pdf" });

            const objectElement = document.getElementById('fileViewer');
            const url = URL.createObjectURL(blob);
            objectElement.src = url;
            //objectElement.width = '1000px';
            //objectElement.height = '1000px';
            //objectElement.type = 'text/plain';
            //var link = document.createElement('a');
            //link.href = window.URL.createObjectURL(blob);
            //link.download = "Report_" + new Date() + ".pdf";
            //link.click();
        }
    };
    xhr.send();
}

function downloadFile(element) {
    var relatedTarget = $(element);
    var file = relatedTarget.data("filename");

    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/masters/ViewFile?fileName=' + file, true);
    xhr.responseType = 'arraybuffer';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: "application/pdf" });

            //const objectElement = document.getElementById('fileViewer');
            //const url = URL.createObjectURL(blob);
            //objectElement.src = url;
            //objectElement.width = '1000px';
            //objectElement.height = '1000px';
            //objectElement.type = 'text/plain';
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = file;
            link.click();
        }
    };
    xhr.send();
}