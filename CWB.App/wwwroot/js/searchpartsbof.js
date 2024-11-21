let radioval = "child";
let bofParts = "";
let tableName = "tbl-existingparts";
var array = new Array();

$(function () {

    array = new Array();
    $("#bofpartno").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#" + tableName+" tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#bofpartdescription").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#" + tableName + " tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 
    $("#bofco").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#" + tableName + " tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 

    $("#DIV_CHLDTABLE").show();
    $("#DIV_ASSMTABLE").hide();
    $("#DIV_BOFTABLE").hide();
    $('#existing-part-bof').on('show.bs.modal', function (event) {
        downloadParts();
    });
    $('input[type=radio][name=radiobof]').change(function () {
        alert(this.value);
    });

    $('input[type=radio][name=radioc]').change(function () {
        alert(this.value);
    });

    $('input[type=radio][name=radioc]').change(function () {
        alert(this.value);
    });


    $('input[type=radio][name=ExistingPartBOFRadio]').change(function () {
        radioval = this.value;
        tableName = "tbl-existingparts";
        if (radioval == "bof") {
            tableName = "tbl-existingpartsbof";
            $("#DIV_CHLDTABLE").hide();
            $("#DIV_ASSMTABLE").hide();
            $("#DIV_BOFTABLE").show();
            downloadParts();
        }
        else {
            if (radioval == "child") {
                $("#DIV_CHLDTABLE").show();
                $("#DIV_ASSMTABLE").hide();
                $("#DIV_BOFTABLE").hide();
                downloadParts();
            }
            else {
                tableName = "tbl-existingparts_assembly";
                $("#DIV_CHLDTABLE").hide();
                $("#DIV_ASSMTABLE").show();
                $("#DIV_BOFTABLE").hide();
            }
        }
    });
});

function downloadParts() {

    var tablebody = $("#tbl-existingpartsbof tbody");
    $(tablebody).html("");//empty tbody
    var tablebody1 = $("#tbl-existingparts tbody");
    $(tablebody1).html("");//empty tbody
    var tablebody2 = $("#tbl-existingparts_assembly tbody");
    let i = 0;
    $(tablebody2).html("");//empty tbody
    array = new Array();
    if (bofParts.length > 0) {
        for (i = 0; i < bofParts.length; i++) {
            if (bofParts[i]['masterPartType'] == "Child") {
                $(tablebody1).append(AppUtil.ProcessTemplateDataNew("ChildPartTemplate", bofParts[i],i));
                array.push(bofParts[i]);
            }
            if (bofParts[i]['masterPartType'] == "BOF") {
                $(tablebody).append(AppUtil.ProcessTemplateDataNew("BOFPartTemplate", bofParts[i],i));
                array.push(bofParts[i]);
            }
            if (bofParts[i]['masterPartType'] == "Assembly") {
                $(tablebody2).append(AppUtil.ProcessTemplateDataNew("AssemblyPartTemplate", bofParts[i],i));
                array.push(bofParts[i]);
            }
        }
    }
    else
    {
        api.get("/masters/selectparts").then((data) => {
            bofParts = data;
            for (i = 0; i < data.length; i++) {
                if (data[i]['masterPartType'] == "Child") {
                    $(tablebody1).append(AppUtil.ProcessTemplateDataNew("ChildPartTemplate", data[i],i));
                    array.push(data[i]);
                }
                if (data[i]['masterPartType'] == "BOF") {
                    $(tablebody).append(AppUtil.ProcessTemplateDataNew("BOFPartTemplate", data[i],i));
                    array.push(data[i]);
                }
                if (data[i]['masterPartType'] == "Assembly") {
                    $(tablebody2).append(AppUtil.ProcessTemplateDataNew("AssemblyPartTemplate", data[i],i));
                    array.push(data[i]);
                }
            }
        }).catch((error) => {
        });
    }
}

    /**
     * if (bofParts.length > 0) {
        if (radioval == "bof") {
            var data = bofParts;
            for (i = 0; i < data.length; i++) {
                if (data[i]['masterPartType'] == "BOF") {
                    $(tablebody).append(AppUtil.ProcessTemplateDataNew("BOFPartTemplate", data[i], i));
                }
            }
        } else {
            for (i = 0; i < data.length; i++) {
                if (data[i]['masterPartType'] == "BOF") {
                    $(tablebody).append(AppUtil.ProcessTemplateDataNew("CAPartTemplate", data[i], i));
                }
            }
        }
    }
    else
     */