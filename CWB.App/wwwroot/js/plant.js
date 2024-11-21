
function LoadHolidays(plantId) {
    var tablebody = $("#HolidaysTable tbody");
    $(tablebody).html("");//empty tbody
    api.get("/plant/getholidays?plantId=" + plantId).then((data) => {
        //console.log(data);
        for (i = 0; i < data.length; i++) {
           $(tablebody).append(AppUtil.ProcessTemplateDataNew("HolidayRow", data[i], i));
        }
    }).catch((error) => {
        //console.log(error);
    });
}
/*
"holidayId": 0,
    "plantId": 0,
    "name": "string",
    "holidayDate": "2024-04-22T19:27:49.041Z",
*/

function EditHoliday(holidayId, name, holiDayDateStr, day) {
    //console.log("holidayId : "+holidayId);
    $("#HolidayId").val(holidayId);
    $("#HName").val(name);
    document.getElementById("HolidayDate").value = holiDayDateStr.split("-").reverse().join("-");;
}

function DeleteHoliday(holidayId,name,plantId) {
    var result = confirm("Are you sure you want to delete "+name+" from holiday list?");
    if (result) {
        api.get("/plant/delholiday?holidayId=" + holidayId).then((data) => {
            //console.log(data);
            var plantId = $("#HolidayPlantId").val();
            LoadHolidays(plantId);
        }).catch((error) => {
        });
    }
}
function AddHolidayToList() {
    var formData = AppUtil.GetFormData("HolidayForm");
    api.post("/plant/plantholiday", formData).then((data) => {
        var plantId = $("#HolidayPlantId").val();
        LoadHolidays(plantId);
        $("#HolidayId").val("0");
        $("#HName").val("");
        $("#HolidayDate").val("");
    }).catch((error) => {
        AppUtil.HandleError("HolidayForm", error);
    });
}

function GetPlantWD(plantId) {
    api.get("/plant/getplantwd?plantId=" + plantId).then((data) => {
        //console.log(data);
        $("#WDId").val(data.wdId);
        $("#WeeklyOff1").val(data.weeklyOff1);
        $("#WeeklyOff2").val(data.weeklyOff2);
        $("#NoOfShifts").val(data.noOfShifts);
        $("#FirstShiftStartTime").val(data.firstShiftStartTime);
        $("#SecondShiftStartTime").val(data.secondShiftStartTime);
        $("#ThirdShiftStartTime").val(data.thirdShiftStartTime);
        $("#FirstShiftDuration").val(data.firstShiftDuration);
        $("#SecondShiftDuration").val(data.secondShiftDuration);
        $("#ThirdShiftDuration").val(data.thirdShiftDuration);
    }).catch((error) => {
        //console.log(error);
    });
}

function AddWorkingDetails() {
    var formData = AppUtil.GetFormData("WDForm");
    api.post("/plant/plantwd", formData).then((data) => {
        //console.log(data);
        $("#WDId").val(data.wdId);
        $("#WDPlantId").val(data.plantId);
        //document.getElementById("WDForm").reset();
    }).catch((error) => {
        AppUtil.HandleError("WDForm", error);
    });
}

function LoadPlants() {
    var tablebody = $("#PlantTable tbody");
    $(tablebody).html("");//empty tbody
    //PlantRowTemplate
    //PlantTable
    api.getbulk("/plant/getplants").then((data) => {
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            for (var key in data[i]) {
                //        console.log(key);
                //      console.log(data[i][key]);
                //    console.log("*****");
            }
            //console.log("================");
           $(tablebody).append(AppUtil.ProcessTemplateDataNew("PlantRowTemplate", data[i], i));

            //var plantID = data[i].plantId;
            //api.get("/plant/getplantwd?plantId=" + plantID).then((data) => {
            //    console.log(data);
            //    WorkDetails.push(data);
            //}).catch((error) => {
            //    console.log('Error occurred:', error.message);
            //});
        }
       
    }).catch((error) => {
    });
};

function DelPlant(name, plantId) {
    let confirmval = confirm("Are your sure you want to delete this plant? : " + name, "Yes", "No");
    if (confirmval) {
        api.get("/plant/delplant?plantId=" + plantId).then((data) => {
            //console.log(data);
            LoadPlants();
        }).catch((error) => {
            //console.log(error);
        });
    }
};
$(function () {

    //address: "test"
    //isMainPlant: true
    //isProductDesigned: true
    //name: "test"
    //notes: "test"
    //plantId: 1
    //tenantId: 1
    //shop-details
    $("#GEN").on("click", function(){
        $("#tab-001").show();
        $("#tab-002").hide();
        $("#tab-003").hide();
    });
    $("#PWD").on("click", function(){
        $("#tab-001").hide();
        $("#tab-002").show();
        $("#tab-003").hide();
    });
    $("#HLI").on("click", function(){
        $("#tab-001").hide();
        $("#tab-002").hide();
        $("#tab-003").show();
    });
    $('#shop-details').on('shown.bs.modal', function (event) {

        if (IsAddOpCalled()) {
            document.getElementById("PlantForm").reset();
            document.getElementById("WDForm").reset();
            document.getElementById("HolidayForm").reset();
            $("#WDPlantId").val("0");
            $("#HolidayPlantId").val("0");
            $("#HolidayId").val("0");
            $("#WDId").val("0");
            var tablebody = $("#HolidaysTable tbody");
            $(tablebody).html("");
            loadCity();
            loadCountrys();
            return;
        }
        $("#GEN").show();
        $("#tab-001").show();
        $("#tab-002").hide();
        $("#tab-003").hide();
        var relatedTarget = $(event.relatedTarget);
        var address = relatedTarget.data("address");
        $("#Address").val(address);
        var isMainPlant = relatedTarget.data("ismainplant");
        var isProductDesigned = relatedTarget.data("isproductdesigned");
        //IsProductDesigned
        //IsMainPlant
        $("#IsMainPlant").prop('checked', false);
        if (isMainPlant) {
            $("#IsMainPlant").prop('checked',true);
        }
        $("#IsProductDesigned").prop('checked', false);
        if (isProductDesigned) {
            $("#IsProductDesigned").prop('checked', true);
        }

        var name = relatedTarget.data("name");
        $("#Name").val(name);
        var city = relatedTarget.data("city");
        $("#City").val(city);
        var pin = relatedTarget.data("pin");
        $("#Pincode").val(pin);
        var gst = relatedTarget.data("gst");
        $("#GstNo").val(gst);
        var pan = relatedTarget.data("pan");
        $("#PanNo").val(pan);
        var country = relatedTarget.data("country");
        $("#Country").val(country);
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
        var plantId = relatedTarget.data("plantid");
        //console.log("PlantId " + plantId);
        $("#PlantId").val(plantId);
        $("#WDPlantId").val(plantId);
        $("#HolidayPlantId").val(plantId);
        var elm = document.getElementById("plantname");
        elm.innerText = name;
        LoadHolidays(plantId);
        GetPlantWD(plantId);
        
    });

    $('#shop-details').on('hide.bs.modal', function (event) {
        document.getElementById("PlantForm").reset();
        document.getElementById("WDForm").reset();
        document.getElementById("HolidayForm").reset();
        document.getElementById("plantname").innerHTML = '';
    });

    $("#docname").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#PlantTable tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    //SaveWorkDetails
    //AddHoliday
    $("#SaveWorkDetails").on('click', function (event) {
        var noofshitfs = document.getElementById('NoOfShifts');
        if (!noofshitfs.value) {
            noofshitfs.style.border = '2px solid red';
            alert(" No Of Shifts Field is Empty");
            return false;
        } else {
            noofshitfs.style.border = '';
        }
        var FirstShiftStartTime = document.getElementById('FirstShiftStartTime');
        if (!FirstShiftStartTime.value) {
            FirstShiftStartTime.style.border = '2px solid red';
            return false;
        } else {
            FirstShiftStartTime.style.border = '';
        }
        var SecondShiftStartTime = document.getElementById('SecondShiftStartTime');
        if (!SecondShiftStartTime.value) {
            SecondShiftStartTime.style.border = '2px solid red';
            return false;
        } else {
            SecondShiftStartTime.style.border = '';
        }
        var ThirdShiftStartTime = document.getElementById('ThirdShiftStartTime');
        if (!ThirdShiftStartTime.value) {
            ThirdShiftStartTime.style.border = '2px solid red';
            return false;
        } else {
            ThirdShiftStartTime.style.border = '';
        }
        var FirstShiftDuration = document.getElementById('FirstShiftDuration');
        if (!FirstShiftDuration.value) {
            FirstShiftDuration.style.border = '2px solid red';
            return false;
        } else {
            FirstShiftDuration.style.border = '';
        }
        var SecondShiftDuration = document.getElementById('SecondShiftDuration');
        if (!SecondShiftDuration.value) {
            SecondShiftDuration.style.border = '2px solid red';
            return false;
        } else {
            SecondShiftDuration.style.border = '';
        }
        var ThirdShiftDuration = document.getElementById('ThirdShiftDuration');
        if (!ThirdShiftDuration.value) {
            ThirdShiftDuration.style.border = '2px solid red';
            return false;
        } else {
            ThirdShiftDuration.style.border = '';
        }
       
        AddWorkingDetails();
        $("#btn-shopdetails-close").prop('disabled', false);
    });
    $("#AddHoliday").on('click', function (event) {
        var nameInput = document.getElementById('HName');
        var dateInput = document.getElementById('HolidayDate');
        

        if (!nameInput.value) {
            nameInput.style.border = '2px solid red';
            return false;
        } else {
            nameInput.style.border = '';
        }

        if (!dateInput.value) {
            dateInput.style.border = '2px solid red';
            return false;
        } else {
            dateInput.style.border = '';
        }
        AddHolidayToList();
        
    });

    $("#btn-shopdetails-close").on('click', function (event) {
             LoadPlants();
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
        dropdownParent: $("#shop-details")
    });
    $("#CountrySelect").select2({
        dropdownParent: $("#shop-details")
    });
    $('#CityPop').on('hidden.bs.modal', function (event) {
        document.getElementById('shop-details').style.filter = 'none';
        $("#CName").val("");
        $("#PCityId").val("");
    });
    $('#CountryPop').on('hidden.bs.modal', function (event) {
        document.getElementById('shop-details').style.filter = 'none';
        $("#CoName").val("");
        $("#PCountryId").val("");
    });
    $('#CityPop').on('show.bs.modal', function (event) {
        document.getElementById('shop-details').style.filter = 'blur(5px)';
    });
    $('#CountryPop').on('show.bs.modal', function (event) {
        document.getElementById('shop-details').style.filter = 'blur(5px)';
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
                    };
                    loadCity();
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
                        id: data.cityId,
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
    $("#BtnSavePlant").on('click', function (event) {
        var Name = document.getElementById('Name');
        if (!Name.value) {
            Name.style.border = '2px solid red';
            return false;
        } else {
            Name.style.border = '';
        }
        var Address = document.getElementById('Address');
        if (!Address.value) {
            $("#Address").val("");
        } else {
            Address.style.border = '';
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
            $("#GstNo").val("");
        } else {
            GstNo.style.border = '';
        }
        var PanNo = document.getElementById('PanNo');
        if (!PanNo.value) {
            $("#PanNo").val("");
        } else {
            PanNo.style.border = '';
        }
        var formData = AppUtil.GetFormData("PlantForm");
        api.post("/plant/plant", formData).then((data) => {
           // console.log(data);
           //document.getElementById("btn-shopdetails-close").click();
            //document.getElementById("PlantForm").reset();
            var wd = $("#WDPlantId").val();
            if (wd === "0") {
                $("#btn-shopdetails-close").prop('disabled', true);
            } else {
                $("#btn-shopdetails-close").prop('disabled', false);
            }
            var plantID = data.plantId;
            $("#WDPlantId").val(plantID);
            $("#HolidayPlantId").val(plantID);
            alert("Please Save the Work Details !");
            $("#tab-002").show();
            $("#tab-001").hide();
        }).catch((error) => {
            AppUtil.HandleError("PlantForm", error);
        });
    });
   
    LoadPlants();
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