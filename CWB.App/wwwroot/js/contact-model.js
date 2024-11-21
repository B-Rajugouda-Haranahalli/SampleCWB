function LoadDivisions(companyId) {
    api.get("/contacts/divisions/" + companyId).then((data) => {
        var tablebody = $("#tbl-division tbody");
        $(tablebody).html("");//empty tbody
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("division-template", data[i]));
            if (ContactsConstants.DivisionId == data[i].divisionId) {
                ContactsFormUtil.PopulateForm(data[i]);
            }
        };
    }).catch((error) => {

    });
};
function LoadCompanies() {
    api.get("/contacts/companies").then((data) => {
        var tablebody = $("#tbl-contacts tbody");
        if (tablebody.length) { //if there is a tablebody in the parent populate it
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("company-template", data[i]));
            }
        }
        if (typeof OnCompDialogHidden === 'function') {
            OnCompDialogHidden();
        }
        //filter once loaded
        ContactsUtil.FilterContacts();
    }).catch((error) => {

    });
};
var ContactsConstants = {
    DivisionId: 0
};
var ContactsFormUtil = {
    UpdateFormIDs: (data) => {
        //////debugger;
        $("#DivisionId").val(data.divisionId);
        $("#CompanyId").val(data.companyId);
    },
    PopulateForm: (data) => {
        //////debugger;
        $("#DivisionId").val(data.divisionId);
        $("#CompanyId").val(data.companyId);
        $("#CompanyType").val(data.companyType);
        $("#CompanyName").val(data.companyName);
        $("#DivisionName").val(data.divisionName);
        $("#DivisionId").val(data.divisionId);
        $("#Location").val(data.location);
        $("#Notes").val(data.notes);
        $("#PlantName").val(data.plantName);
        $("#City").val(data.city);
        $("#Pincode").val(data.pincode);
        $("#Country").val(data.country);
        $("#GstNo").val(data.gstNo);
        $("#PanNo").val(data.panNo);
    },
    PopulateFormFromRelatedTarget: (relatedTarget) => {
        //////debugger;
        $("#DivisionId").val(relatedTarget.data("divisionid"));
        $("#CompanyId").val(relatedTarget.data("id"));
        $("#CompanyType").val(relatedTarget.data("companytype"));
        $("#CompanyName").val(relatedTarget.data("companyname"));
        $("#DivisionName").val(relatedTarget.data("divisionname"));
        $("#Location").val(relatedTarget.data("location"));
        $("#PlantName").val(relatedTarget.data("plantname"));
        $("#Pincode").val(relatedTarget.data("pin"));
        $("#GstNo").val(relatedTarget.data("gst"));
        $("#PanNo").val(relatedTarget.data("pan"));
    },
    UpdateDivisonTable: (companyId, divisionId) => {
        //////debugger;
        ContactsConstants.DivisionId = divisionId;
        LoadDivisions(companyId);
        /*api.get("/contacts/divisions/" + companyId).then((data) => {
            var tablebody = $("#tbl-division tbody");
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("division-template", data[i]));
                if (ContactsConstants.DivisionId == data[i].divisionId) {
                    ContactsFormUtil.PopulateForm(data[i]);
                }
            };
        }).catch((error) => {

        });*/
    },
    ClearConstants: () => {
        ContactsConstants.DivisionId = 0;
    },
    ClearForm: () => {
        $("#DivisionId").val("0");
        $("#CompanyId").val("0");
        $("#CompanyType").val("");
        $("#CompanyName").val("");
        $("#DivisionName").val("");
        $("#DivisionId").val("0");
        $("#Location").val("");
        $("#PlantName").val("");
        $("#City").val("");
        $("#Pincode").val("");
        $("#Country").val("");
        $("#GstNo").val("");
        $("#PanNo").val("");
    },
    ClearDivision: () => {
        $("#DivisionId").val("0");
        $("#DivisionName").val("");
        $("#City").val("");
        $("#Country").val("");
    },
    HasFunction: (obj, methodName) => {
        return ((typeof obj[methodName]) == "function");
    }
};


function DeleteCompany(companyID) {
    let confirmval = confirm("Are your sure you want to delete this company?", "Yes", "No");
    if (confirmval) {
        api.get("/contacts/deletecompany?companyID=" + companyID).then((data) => {
            LoadCompanies();
        }).catch((error) => {

        });
    }
};

function DeleteDivision(divisionId, companyId) {
    let confirmval = confirm("Are your sure you want to delete this division?", "Yes", "No");
    if (confirmval) {
        api.get("/contacts/deletedivision?divisionID=" + divisionId).then((data) => {
            LoadDivisions(companyId);
        }).catch((error) => {

        });
    }
};
function SetDivisionEditValues(divisionId, divisionName, location, notes, companyId, companyType, name, plantName, city, pincode, country, gstNo, panNo) {
    var data = {};
    data['divisionId'] = divisionId;
    data['divisionName'] = divisionName;
    data['location'] = location;
    data['companyId'] = companyId;
    data['companyName'] = name;
    data['companyType'] = companyType;
    data['plantName'] = plantName;
    data['city'] = city;
    data['pincode'] = pincode;
    data['country'] = country;
    data['gstNo'] = gstNo;
    data['panNo'] = panNo;
    ContactsFormUtil.PopulateForm(data);
};

$(function () {
    $("#btnAddDivision").hide();
    $('#dialog-company').on('show.bs.modal', function (event) {
        ContactsFormUtil.ClearForm();
        ContactsFormUtil.ClearConstants();
        var relatedTarget = $(event.relatedTarget);
        var companyId = relatedTarget.data("id");
        var divisionId = relatedTarget.data("divisionid");
        if (companyId == "0") {
            loadCity();
            loadCountrys();
            var tablebody = $("#tbl-division tbody");
            $(tablebody).html("");//empty tbody
            $("#btnAddDivision").hide();
            return;
        }
        else {
            ContactsFormUtil.PopulateFormFromRelatedTarget(relatedTarget);
            var CitySelect = $('#CitySelect');
            CitySelect.html('');
            api.getbulk("/Plant/GetCitys").then((data) => {
                var sv = "";
                div_data = "<option value='" + sv + "'>" + "--Select--" + "</option>";
                CitySelect.append(div_data);
                for (i = 0; i < data.length; i++) {
                    div_data = "<option value='" + data[i].name + "'>" + data[i].name + "</option>";
                    CitySelect.append(div_data);
                }
                var city = relatedTarget.data("city");
                var CitySe = $("#CitySelect");
                CitySe.find("option[value='" + city + "']").prop('selected', true);
            });
            var selElem = $('#CountrySelect');
            selElem.html('');
            api.getbulk("/Plant/GetCountrys").then((data) => {

                for (i = 0; i < data.length; i++) {
                    div_data = "<option value='" + data[i].name + "'>" + data[i].name + "</option>";
                    selElem.append(div_data);
                }
                var country = relatedTarget.data("country");
                var CountrSe = $("#CountrySelect");
                CountrSe.find("option[value='" + country + "']").prop('selected', true);
            });
        }
        $("#btnAddDivision").show();
        ContactsFormUtil.UpdateDivisonTable(companyId, divisionId);
    });
    $('#dialog-company').on('hide.bs.modal', function (event) {
        ContactsFormUtil.ClearForm();
        ContactsFormUtil.ClearConstants();
        $("#CompanyName-error").text(" ");
        var CompanyType = document.getElementById('CompanyType');
        CompanyType.style.border = '';
        var CompanyName = document.getElementById('CompanyName');
        CompanyName.style.border = '';
        var DivisionName = document.getElementById('DivisionName');
        DivisionName.style.border = '';
        var PlantName = document.getElementById('PlantName');
        PlantName.style.border = '';
        //var City = document.getElementById('City');
        //City.style.border = '';
        //var Country = document.getElementById('Country');
        //Country.style.border = '';

    //    alert("On hide..");
       // $("#frmCompany").resetValidation();
        LoadCompanies();
    });

    /*$("#btnContactClose").click(function () {
        $("#dialog-company").dialog("close");
    });*/

    $("#btnContactSubmit").on('click',function () {
     //   //debugger;
        var CompanyType = document.getElementById('CompanyType');
        if (!CompanyType.value) {
            CompanyType.style.border = '2px solid red';
            return false;
        } else {
            CompanyType.style.border = '';
        }

        var CompanyNameV = document.forms["frmCompany"]["CompanyName"].value;
        var CompanyName = document.getElementById('CompanyName');
        if (!CompanyNameV) {
            CompanyName.style.border = '2px solid red';
            return false;
        } else {
            CompanyName.style.border = '';
        }
        var DivisionName = document.getElementById('DivisionName');
        if (!DivisionName.value) {
            DivisionName.style.border = '2px solid red';
            return false;
        } else {
            DivisionName.style.border = '';
        }
        //var PlantName = document.getElementById('PlantName');
        //if (!PlantName.value) {
        //    PlantName.style.border = '2px solid red';
        //    return false;
        //} else {
        //    PlantName.style.border = '';
        //}
        $("#PlantName").val("ps");
        var Location = document.getElementById('Location');
        if (!Location.value) {
            $("#Location").val(" ");
        } else {
            Location.style.border = '';
        }
        var City = document.getElementById('CitySelect');
        if (!City.value) {
            var newNamevalidate = $('#CitySelect').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
            return false;
        } else {
            var newNamevalidate = $('#CitySelect').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        var Pincode = document.getElementById('Pincode');
        if (!Pincode.value) {
            $("#Pincode").val("");
        } else {
            Pincode.style.border = '';
        }
        var Country = document.getElementById('CountrySelect');
        if (!Country.value) {
            var newNamevalidate = $('#CountrySelect').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
            return false;
        } else {
            var newNamevalidate = $('#CountrySelect').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        var GstNo = document.getElementById('GstNo');
        if (!GstNo.value) {
            $("#GstNo").val(" ");
        } else {
            GstNo.style.border = '';
        }
        var PanNo = document.getElementById('PanNo');
        if (!PanNo.value) {
            $("#PanNo").val(" ");
        } else {
            PanNo.style.border = '';
        }
        if ($("#frmCompany").valid()) {
            var formData = AppUtil.GetFormData("frmCompany");
            api.post("/contacts/company", formData).then((data) => {
                var cid = $("#CompanyId").val();
                if (cid == 0 || cid == "0") {
                    ContactsFormUtil.ClearForm();
                }
                ContactsFormUtil.UpdateFormIDs(data);
                ContactsFormUtil.UpdateDivisonTable(data.companyId, data.divisionId);
                $("#btnAddDivision").show();
                ContactsFormUtil.ClearConstants();
                if (typeof OnCompanyCreated === 'function') {
                    OnCompanyCreated(data);
                }
                else {
                    LoadCompanies();
                    LoadDivisions(data.companyId);
                }
            }).catch((error) => {
                AppUtil.HandleError("frmCompany", error);
            });
        }
    });
    $("#EditCityPop").click(function (event) {
        var selectedValue = $("#CitySelect").val();  // Get the value of the selected option
        var selectedText = $("#CitySelect").find("option:selected").text();
        if (selectedText == "--Select--") {
            $("#CityPop").modal("hide");
            return false;
        }
        document.forms["frmAddCity"]["Name"].value = selectedText;
        api.getbulk("/Plant/GetCitys").then((data) => {
            data = data.filter(item => item.name == selectedText);
            var cid = data[0].cityId;
            document.forms["frmAddCity"]["CityId"].value = cid;
        });
    });
    $("#EditCountryPop").click(function (event) {
        var selectedValue = $("#CountrySelect").val();  // Get the value of the selected option
        var selectedText = $("#CountrySelect").find("option:selected").text();
        document.forms["frmAddCountry"]["Name"].value = selectedText;
        api.getbulk("/Plant/GetCountrys").then((data) => {
            data = data.filter(item => item.name == selectedText);
            var cid = data[0].countryId;
            document.forms["frmAddCountry"]["CountryId"].value = cid;
        });
    });
    $("#CitySelect").select2({
        dropdownParent: $("#dialog-company")
    });
    $("#CountrySelect").select2({
        dropdownParent: $("#dialog-company")
    });
    $('#CityPop').on('hidden.bs.modal', function (event) {
        document.getElementById('dialog-company').style.filter = 'none';
        $("#CName").val("");
        $("#PCityId").val("");
    });
    $('#CountryPop').on('hidden.bs.modal', function (event) {
        document.getElementById('dialog-company').style.filter = 'none';
        $("#CoName").val("");
        $("#PCountryId").val("");
    });
    $('#CityPop').on('show.bs.modal', function (event) {
        document.getElementById('dialog-company').style.filter = 'blur(5px)';
    });
    $('#CountryPop').on('show.bs.modal', function (event) {
        document.getElementById('dialog-company').style.filter = 'blur(5px)';
    });
    
    $("#SaveCity").on('click', function () {
        var name = $("#CName").val();
        if (name.length == 0) {
            var newNamevalidate = document.getElementById('CName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('CName');
            newNamevalidate.style.border = '';
        }
        var formData = AppUtil.GetFormData("frmAddCity");
       // if (valid) {

            api.getbulk("/plant/CheckCity?city=" + name).then((data) => {
                if (!data) {
                    api.post("/plant/PostCity", formData).then((data) => {
                        var newopt = {
                            id: data.cityId,
                            text: data.name
                        }; loadCity();
                        //$('#CitySelect').append(newOption).trigger('change');
                        //  $('#UOMId').val(newCo);
                        //loadUOMs("UOMId");
                        //document.getElementById("btn_adduom_close").click();
                        $("#CityPop").modal("hide");
                    }).catch((error) => {
                    });
                } else {
                    var newNamevalidate = document.getElementById('CName');
                    newNamevalidate.style.border = '2px solid red';
                }
            }).catch((error) => {
            });
       // }
    });
    $("#SaveCountry").on('click', function () {
        var name = $("#CoName").val();
        if (name.length == 0) {
            var newNamevalidate = document.getElementById('CoName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('CoName');
            newNamevalidate.style.border = '';
        }
        var formData = AppUtil.GetFormData("frmAddCountry");
       // if (valid) {

        api.getbulk("/plant/CheckCountry?city=" + name).then((data) => {
                if (!data) {
                    api.post("/plant/PostCountry", formData).then((data) => {
                        var newopt = {
                            id: data.countryId,
                            text: data.name
                        };
                        loadCountrys();
                        $("#CountryPop").modal("hide");
                    }).catch((error) => {
                    });
                } else {
                    var newNamevalidate = document.getElementById('CoName');
                    newNamevalidate.style.border = '2px solid red';
                }
            }).catch((error) => {
            });
       // }
    });
    $("#btnAddDivision").on('click',function () {
        //////debugger;
        ContactsFormUtil.ClearDivision();
        ContactsFormUtil.ClearConstants();
    });
});

function loadCity() {
    var selElem = $('#CitySelect');
    selElem.html('');
    api.getbulk("/Plant/GetCitys").then((data) => {
        var sv = "";
        div_data = "<option value='" + sv + "'>" + "--Select--" + "</option>";
        selElem.append(div_data);
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].name + "'>" + data[i].name + "</option>";
            selElem.append(div_data);
        }
    });

}
function loadCountrys() {
    var selElem = $('#CountrySelect');
    selElem.html('');
    api.getbulk("/Plant/GetCountrys").then((data) => {
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].name + "'>" + data[i].name + "</option>";
            selElem.append(div_data);
        }
    });

}