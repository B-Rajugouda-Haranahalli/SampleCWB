function loadRMTypes(RMTypeElement) {//pass the element name
    var compSelect = $('#' + RMTypeElement);
    compSelect.empty();
    var div_data = "<option value=''></option>";
    compSelect.append(div_data);
    api.get("/masters/rmtypes").then((data) => {
        //var div_data = "<option value=""></option>";
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].rawMaterialTypeId + "'>" +
                data[i].name +
                "</option>";
            //console.log(div_data);
            compSelect.append(div_data);
        }
        //compSelect.html(div_data);
        //filter once loaded

    }).catch((error) => {
        //console.log(JSON.stringfy(error));
    });
}

function loadRMStandards(RMStandardElement) {//pass the element name
    var compSelect = $('#' + RMStandardElement);
    compSelect.empty();
    var div_data = "<option value=''></option>";
    compSelect.append(div_data);
    api.get("/masters/rmstandards").then((data) => {
        //var div_data = "<option value=""></option>";
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].standard + "'>" +
                data[i].name +
                "</option>";
            //console.log(div_data);
            compSelect.append(div_data);
        }
        //compSelect.html(div_data);
        //filter once loaded

    }).catch((error) => {
        //console.log(JSON.stringfy(error));
    });
}

function loadRMSpecs(RMSpeElement) {//pass the element name
    var compSelect = $('#' + RMSpeElement);
    compSelect.empty();
    var div_data = "<option value=''></option>";
    compSelect.append(div_data);
    api.get("/masters/rmspecs").then((data) => {
        //var div_data = "<option value=""></option>";
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].materialSpecId + "'>" +
                data[i].name +
                "</option>";
          //  //console.log(div_data);
            compSelect.append(div_data);
        }
        //compSelect.html(div_data);
        //filter once loaded

    }).catch((error) => {
        //console.log(JSON.stringfy(error));
    });
}

function loadBaseRMs(BaseEMElement) {//pass the element name
    var compSelect = $('#' + BaseEMElement);
    compSelect.empty();
    var div_data = "<option value=''></option>";
    compSelect.append(div_data);
    api.get("/masters/baserms").then((data) => {
        //var div_data = "<option value=""></option>";
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].baseRawMaterialId + "'>" +
                data[i].name +
                "</option>";
            //console.log(div_data);
            compSelect.append(div_data);
        }
        //compSelect.html(div_data);
        //filter once loaded

    }).catch((error) => {
        //console.log(JSON.stringfy(error));
    });
}


