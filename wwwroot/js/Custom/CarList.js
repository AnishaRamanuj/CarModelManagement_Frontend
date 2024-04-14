var token = getToken();
var validator;
var form = document.getElementById('carForm');
//var ID = userId;
$(document).ready(function () {
  
  GetCarData();
});
function GetCarData(){
  debugger
  $('#CarsTable').DataTable({
    paging: true,
    responsive: true,
    //bDestroy: true,
    //"ordering": true,
    //processing: true,
    //serverSide: false,
    //scrollY: 530,
    //language: {
    //  processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="font-size:30px; color:#009ef7;"></i><span class="sr-only">Loading...</span>',
    //  loadingRecords: ""
    //},
    "dom":
      "<'row'" +
      "<'col-sm-12 d-flex align-items-center justify-content-end'f>" +
      ">" +

      "<'table-responsive'tr>" +

      "<'row'" +
      "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'li>" +
      "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
      ">",
    "ajax": {
      "headers": {
        "Authorization": token
      },
      "url": '/Home/GetAllCars/',
      "type": "get",
      "datatype": "json"
    },
    "columns": [
      {
        "data": "brand",
        "autoWidth": true,
        "searchable": true
      },
      {
        "data": "class",
        "autoWidth": true,
        "searchable": true
      },
      {
        "data": "modelCode",
        "autoWidth": true,
        "searchable": true
      },
      {
        "data": "description",
        "autoWidth": true,
        "searchable": true
      },
      {
        "data": "price",
        "autoWidth": true,
        "searchable": true
      },
      {
        "data": "manufacturedOn",
        "autoWidth": true,
        "searchable": true
      },
      {
        data: "isActive",
        autoWidth: true,
        "searchable": true,
        render: function (data, type, row) {
          return '<td><span>' + (row.isActive == true ? 'Active' : 'InActive') + '</span></td>'
        }
      },
      //{
      //  data: "carId",
      //  "searchable": false,
      //  sortable: false,
      //  render: function (data, type, row) {
      //    return '<td class="text-end"><a  onclick="EditById(\'' + row.carId + '\')" class="btn btn-sm btn-white text-success me-2"><i class="fa fa-edit"></i> Edit</a><a onclick="deleteConfirm(\'' + row.carId + '\')"  class="btn btn-sm btn-white text-danger"><i class="fa fa-trash"></i>Delete</a></td>';
      //  }
      //}
    ]
  });
}
//$('#btnSubmit').click(function () {
//  debugger
//  e.preventDefault();

//  //if (validator) {
//  //  validator.validate().then(function (status) {
//  //    console.log('validated!');

//        var formData = new FormData();
//        formData.append("Brand", $("#ddlBrand").val())
//        formData.append("Class", $("#ddlClass").val())
//        formData.append("ModelName", $("#txtModelName").val())
//        formData.append("ModelCode", $("#txtModelCode").val())
//        formData.append("Description", $("#txtDescription").val())
//        formData.append("Features", $("#txtFeatures").val())
//        formData.append("Price", $("#txtPrice").val())
//        formData.append("ManufacturedOn", $("#txtManufacturedOn").val())
//        formData.append("IsActive", $("#chkIsActive").val())
//        $.ajax({
//          headers: {
//            Authorization: token
//          },
//          url: 'Home/AddCar',
//          type: "post",
//          data: formData,
//          contentType: false,
//          processData: false,
//          success: function (data) {

//            toastr.success(data.message);
//            document.getElementById("menuForm").reset();
//            GetCarData();
//            $('#add_event').modal('hide');
//          },
//          error: function (err) {
//            toastr.error('An error has occured!!!');

//          }
//        });
//      //}
//   // })
// // }
//});

$(document).on("click", "#AddCarModel", function () {
    bindddlGetAllMenu();
    bindGetAllMenuType();
    $('#add_event').modal('show');
    $('.carTitle').html('Add Car');
    document.getElementById("carForm").reset();
    validator.resetForm();
    $("#btnUpdate").hide();
    $("#btnSubmit").show();
});

function getToken() {
    var token = getCookie("Multiverse-Token");
    return token;
}
function getCookie(cname) {

    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}