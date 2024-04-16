var validator;
var form = document.getElementById('carForm');
$(document).ready(function () {
  toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
  };

  GetCarData();
});

function GetCarData(){
  $('#CarsTable').DataTable({
    paging: true,
    responsive: true,
    bDestroy: true,
    "ordering": true,
    processing: true,
    serverSide: false,
    scrollY: 530,
    language: {
      processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="font-size:30px; color:#009ef7;"></i><span class="sr-only">Loading...</span>',
      loadingRecords: ""
    },
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
        "data": "features",
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
        "searchable": true,
        "render": function (data) {
          return moment(data).format('DD/MM/YYYY');
        }

      },
      {
        data: "isActive",
        autoWidth: true,
        "searchable": true,
        render: function (data, type, row) {
          return '<td><span>' + (row.isActive == true ? 'Active' : 'InActive') + '</span></td>'
        }
      },
      {
        data: "carId",
        "searchable": false,
        sortable: false,
        render: function (data, type, row) {
          return '<td class="text-end"><a  onclick="EditById(\'' + row.carId + '\')" class="btn btn-sm btn-white text-success me-2"><i class="fa fa-edit"></i> Edit</a><a onclick="deleteConfirm(\'' + row.carId + '\')"  class="btn btn-sm btn-white text-danger"><i class="fa fa-trash"></i>Delete</a></td>';
        }
      }
    ]
  });
}

$('#btnSubmit').click(function (e) {
  e.preventDefault();
  
  var formData = new FormData();
        formData.append("CarId", $("#txtCarId").val())
        formData.append("Brand", $("#ddlBrand").val().toString());
        formData.append("Class", $("#ddlClass").val());
        formData.append("ModelName", $("#txtModelName").val());
        formData.append("ModelCode", $("#txtModelCode").val());
        formData.append("Description", $("#txtDescription").val());
        formData.append("Features", $("#txtFeatures").val());
        formData.append("Price", $("#txtPrice").val());
        formData.append("ManufacturedOn", $("#txtManufacturedOn").val());
       formData.append("IsActive", $("#chkIsActive").prop('checked'));
  
        // Append each selected file to the FormData object
        var files = $("#MultipleFiles")[0].files;
        for (var i = 0; i < files.length; i++) {
          formData.append("MultipleFiles", files[i]);
        }

        $.ajax({
          url: 'Home/AddCar',
          type: "post",
          data: formData,
          contentType: false,
          processData: false,
          success: function (data) {
            debugger
            if (data.success === 'Success') {
              toastr.success(data.message);
              document.getElementById("carForm").reset();
              GetCarData();
              $('#add_event').modal('hide');
            } else {
              data.errors.forEach(error => toastr.error(error));
            }
           
          },
          error: function (err) {
            toastr.error('An error has occurred!!!');
          }
        });
});


$(document).on("click", "#AddCarModel", function () {
    $('#add_event').modal('show');
    $('.carTitle').html('Add Car');
    document.getElementById("carForm").reset();
    $("#btnUpdate").hide();
    $("#btnSubmit").show();
});

function EditById(id) {
  $('.carTitle').html('Edit Car Details');
  $('#add_event').modal('show');
  $("#btnUpdate").show();
  $("#btnSubmit").hide();
  var formData = new FormData();
  formData.append('id', id);
  $.ajax({
    url: '/Home/GetAllCars/',
    dataType: "json",
    type: 'GET',
    data: { id: id },
    success: function (data) {
      
      var carData = null;
      for (var i = 0; i < data["data"].length; i++) {
        if (data["data"][i].carId == id) {
          carData = data["data"][i];
          break;
        }
      }
      if (carData) {
        
        $("#txtCarId").val(carData.carId);
        $("#txtModelName").val(carData.modelName);
        $("#txtModelCode").val(carData.modelCode);
        $("#txtDescription").val(carData.description);
        $("#ddlBrand").val(carData.carId).change();
        $("#txtFeatures").val(carData.features);
        $("#txtPrice").val(carData.price);
        var manufacturedOnDate = new Date(carData.manufacturedOn);
        var formattedManufacturedOn = manufacturedOnDate.toISOString().split('T')[0];
        $("#txtManufacturedOn").val(formattedManufacturedOn);
        $("#chkIsActive").prop("checked", carData.isActive);
        $("#ddlBrand").val(carData.brand.trim()).change();

        $("#ddlClass").val(carData.class.trim()).change();
      }
      else {
        toastr.error('Car with id ' + id + ' not found');
      }
    },
    error: function (err) {
      toastr.error('An error has occured!!!');
      console.log("err---->" + err);

    }
  });
}

var deleteConfirm = function (valID) {
  $('#DeleteId').val(valID);
  $('#DeleteModal').modal('show');
}

function DeleteCar(flag) {
  if (flag == true) {

    var CarId = $('#DeleteId').val();

    $.ajax({
    
      type: "POST",
      dataType: "json",
      url: "/Home/DeleteCar?car=" + CarId,
      success: function (data) {
        debugger
        if (data.success == "Deleted") {
          toastr.success(data.message);
          document.getElementById("carForm").reset();
          $('#DeleteModal').modal('hide');
          GetCarData();
        }
        else {
          toastr.error(data.message);
        }
      },
      error: function (response) {
        alert(response.d);
      }
    });
  }
}

$(document).on('click', '#btnUpdate', function (e) {
  debugger
  e.preventDefault();

        var formData = new FormData();
        formData.append("CarId", $("#txtCarId").val())
        formData.append("Brand", $("#ddlBrand").val().toString())
        formData.append("Class", $("#ddlClass").val())
        formData.append("ModelName", $("#txtModelName").val())
        formData.append("ModelCode", $("#txtModelCode").val())
        formData.append("Description", $("#txtDescription").val())
        formData.append("Features", $("#txtFeatures").val())
        formData.append("Price", $("#txtPrice").val())
        formData.append("ManufacturedOn", $("#txtManufacturedOn").val())
        formData.append("IsActive", $("#chkIsActive").prop('checked'));

        $("#btnUpdate").hide();
        $("#btnSubmit").show();

        $.ajax({
          url: "/Home/UpdateCar",
          type: "Post",
          data: formData,
          contentType: false,
          processData: false,
          success: function (data) {
            debugger
            toastr.success(data.message);
            console.log(data);
            document.getElementById("carForm").reset();
            GetCarData();
            $('#add_event').modal('hide');
          },
          error: function (err) {
            toastr.error('An error has occured!!!');
          }
        });

})