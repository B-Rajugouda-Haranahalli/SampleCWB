function loadDocumentTypes() {
    //DocumentTypeTable
    //DocumentTypeTemplate
    var tablebody = $("#DocumentTypeTable tbody");
    $(tablebody).html("");//empty tbody
    //PlantRowTemplate
    //PlantTable
    api.getbulk("/documenttype/getdoctypes").then((data) => {
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            for (var key in data[i]) {
                //        console.log(key);
                //      console.log(data[i][key]);
                //    console.log("*****");
            }
            //console.log("================");
            $(tablebody).append(AppUtil.ProcessTemplateDataNew("DocumentTypeTemplate", data[i], i));
        }
    }).catch((error) => {
    });
};

    


function DelDocType(name, doctypeId) {
    let confirmval = confirm("Are your sure you want to delete this doctype? : " + name, "Yes", "No");
    if (confirmval) {
        api.get("/documenttype/deldoctype?docTypeId=" + doctypeId).then((data) => {
            //console.log(data);
            loadDocumentTypes();
        }).catch((error) => {
            //console.log(error);
        });
    }
};

$(function () {

    //data - name="{name}" data - doctypeid="{documentTypeId}"
    //data - description="{description}" data - extension="{extension}"
    //data - isUploadedByUser="{isUploadedByUser}"
    $('#document-type-details').on('show.bs.modal', function (event) {
        if (IsAddOpCalled())
            return;
        var relatedTarget = $(event.relatedTarget);
        var val = relatedTarget.data("name");
        $("#Name").val(val);
        val = relatedTarget.data("doctypeid");
        $("#DocumentTypeId").val(val);
        val = relatedTarget.data("description");
        $("#Description").val(val);
        val = relatedTarget.data("extension");
        $("#Extension").val(val);
        val = relatedTarget.data("isUploadedByUser");
        $("#IsUploadedByUser").val(val);
    });

    $("#doctypename").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocumentTypeTable tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#BtnSaveDocumentType").on("click",function (event) {
        var formData = AppUtil.GetFormData("DocTypeForm");
        var form = document.getElementById("DocTypeForm");

        if (form.checkValidity()) { }
        else { return; }
        
        api.post("/documenttype/doctype", formData).then((data) => {
            //console.log(data);
            document.getElementById("btn-doctypedetails-close").click();
            document.getElementById("DocTypeForm").reset();
            loadDocumentTypes();
        }).catch((error) => {
            AppUtil.HandleError("DocTypeForm", error);
        });
    });

    
    loadDocumentTypes();
    
});

