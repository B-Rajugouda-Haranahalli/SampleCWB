//bofco
//bofpartno
//bofpartdescription

$(function () {

    
    $("#eppn").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbl-existingparts tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });   

    $("#eppd").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbl-existingparts tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 

    $("#CPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#CustRMTable tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 

    $("#CPartDescription").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#CustRMTable tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 


    $("#OPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#OwnRMTable tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#OAdditionalInfo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#OwnRMTable tbody tr").filter(function () {
            $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#OSupplierId").change(function () {
        var value = $("#OSupplierId option:selected").text();
     //   alert(value);
        $("#OwnRMTable tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

});
