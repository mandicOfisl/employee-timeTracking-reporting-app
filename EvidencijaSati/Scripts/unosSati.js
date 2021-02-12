const regex = new RegExp('^\\d{1,2}[:]\\d{1,2}$');

$(document).ready(function () {
	 var aktivanProjekt = $('#aktivanProjekt').val();

	 $('#btnPredaj').prop('disabled', true);
	 $('#btnOdustani').prop('disabled', true);
	 $('#btnSpremi').prop('disabled', true);

	 if (aktivanProjekt > -1) {
		  $('button.modal-link').prop('disabled', true);
		  var btnId = "btnProjekt" + aktivanProjekt;
		  $('button.btnTimer').each(function (i) {
				if ($(this).attr('id') == btnId) {
					 $(this).removeClass('btn-success');
					 $(this).addClass('btn-danger');
					 $(this).html('Stop');
					 $('button.btn-success').prop('disabled', true);
				}
		  });
	 }

	 lockTextFields(true);
	 calculateTotals();

	 var projectId, start, end, activeSatnicaId, satnicaId;

	 satnicaId = $('#satnicaId').val();

	 $("#table").DataTable({
		  select: true,
		  paging: false,
		  ordering: false,
		  info: false,
		  searching: false,
		  columnDefs: [
				{
					 targets: 1,
					 className: 'dt-body-center'
				},
				{
					 targets: 2,
					 className: 'dt-body-center'
				},
				{
					 targets: 3,
					 className: 'dt-body-center'
				},
				{
					 targets: 4,
					 className: 'dt-body-center'
				},
				{
					 targets: 5,
					 className: 'dt-body-center'
				}
		  ]
	 });

	 $('button.btnTimer').click(function (e) {
		  e.preventDefault();
		  projectId = this.id.split('btnProjekt')[1];

		  if ($(this).hasClass('btn-success')) {
				start = new Date();

				$(this).removeClass('btn-success');
				$(this).addClass('btn-danger');
				$(this).html('Stop');

				$('#btnOdustani').prop('disabled', true);
				$('button.modal-link').prop('disabled', true);
				$('button.btn-success').prop('disabled', true);

				var model = {
					 IDSatnicaProjekta: -1,
					 SatnicaID: satnicaId,
					 ProjektID: projectId,
					 Start: start,
					 End: null
				};

				$.ajax({
					 url: "/Satnica/SpremiTempSatnicu",
					 method: "POST",
					 data: JSON.stringify(model),
					 contentType: 'application/json',
					 success: function (res) {
						  activeSatnicaId = res[2];
					 }
				});

		  } else {
				end = new Date();

				$('button.btn-success').prop('disabled', false);

				$(this).removeClass('btn-danger');
				$(this).addClass('btn-success');
				$(this).html('Start');

				calculateTotals();
				$('#btnOdustani').prop('disabled', false);

				var model = {
					 IDSatnicaProjekta: activeSatnicaId,
					 SatnicaID: satnicaId,
					 ProjektID: projectId,
					 Start: start,
					 End: end
				};

				$.ajax({
					 url: "/Satnica/ZabiljeziKraj",
					 method: "POST",
					 data: JSON.stringify(model),
					 contentType: 'application/json',
					 success: function (res) {
						  var id = "txtZab" + projectId;
						  document.getElementById(id).innerHTML =
								addHoursMinutes(document.getElementById(id).innerHTML.trim(), res);
						  calculateTotals();
						  $('button.modal-link').prop('disabled', false);
					 }
				});
		  }

	 });

	 $('#btnUredi').on('click', function (e) {
		  e.preventDefault();
		  lockTextFields(false);
		  $('#btnOdustani').prop('disabled', false);
	 });

	 $('#btnSpremi').on('click', function (e) {
		  e.preventDefault();

		  lockTextFields(true);

		  var totR = document.getElementById("txtTotalRedovni").innerHTML.trim();
		  var totP = document.getElementById("txtTotalPrekovremeni").innerHTML.trim();;

		  var model = {
				IDSatnica: $('#satnicaId').val(),
				DjelatnikID: $('#djelatnikId').val(),
				Datum: new Date(),
				ProjektZabiljezeno: getProjektZabiljezeno(),
				Total: parseStringMinutesToFloat(addHoursMinutes(totR, totP)),
				TotalRedovni: parseStringMinutesToFloat(totR),
				TotalPrekovremeni: parseStringMinutesToFloat(totP),
				Komentar: document.getElementById("txtKomentar").value
		  };

		  $.ajax({
				url: "/Satnica/SpremiZaPredaju",
				method: "POST",
				data: JSON.stringify(model),
				contentType: 'application/json',
				success: function (res) {
					 if (res > 0) {
						  $('#btnPredaj').prop('disabled', false);
					 }
				}
		  });
	 });

	 $('#btnOdustani').on('click', function (e) {
		  e.preventDefault();
		  if (confirm("Obrisati sve podatke?")) {
				$('button.btnTimer').prop('disabled', false);
				$('input').each(function (i) {
					 $(this).val("");
				});
				$('.zabiljezeno').each(function (i) {
					 $(this).html('00:00');
				});
				document.getElementById("txtTotalRedovni").innerHTML = '00:00';
				document.getElementById("txtTotalPrekovremeni").innerHTML = '00:00';
				lockTextFields(true);
				$('#btnSpremi').prop('disabled', true);
				$('#btnPredaj').prop('disabled', true);
				$(this).prop('disabled', true);
		  }
	 });

	 $('#btnPredaj').on('click', function (e) {
		  e.preventDefault();
		  if (confirm("Predati unešene podatke?")) {
				$('#btnPredaj').prop('disabled', true);
				$('#btnOdustani').prop('disabled', false);
				$('button.btnTimer').prop('disabled', false);
				$('input').each(function (i) {
					 $(this).val("");
				});

				lockTextFields(true);

				$.ajax({
					 url: "/Satnica/PredajNaProvjeru/" + satnicaId,
					 method: "POST",
					 success: function (res) {
						  $('<div id="modal-container" class="modal fade">' +
								'<div class="modal-dialog" role="document">' +
								res + '</div></div> ').modal();
						  $('#btnPredaj').prop('disabled', false);
					 }
				});
		  }
	 });				

});

$(function () {
	 $('body').on('click', '.modal-link', function (e) {
		  e.preventDefault();
		  $("#modal-container").remove();
		  $.get($(this).data("targeturl"), function (data) {
				$('<div id="modal-container" class="modal fade">' +
					 '<div class="modal-dialog" role="document">' +
					 data + '</div></div> ').modal();
		  });
	 });
});

function calculateDifference(first, second) {
	 var f = (first != '' && regex.test(first)) ? first.split(':') : ['00', '00'];
	 var s = (second != '' && regex.test(second)) ? second.split(':') : ['00', '00'];

	 var fH = parseInt(f[0]);
	 var sH = parseInt(s[0]);
	 var fM = parseInt(f[1]);
	 var sM = parseInt(s[1]);

	 var newH = sH - fH;
	 var newM = sM - fM;

	 if (newM < 0) {
		  newH -= 1;
		  newM += 60;
	 }

	 return newH.toString().padStart(2, '0') + ":" + newM.toString().padStart(2, '0');
}

function addHoursMinutes(first, second) {
	 var f = (first != '' && regex.test(first)) ? first.split(':') : ['00', '00'];
	 var s = (second != '' && regex.test(second)) ? second.split(':') : ['00', '00'];

	 var fH = parseInt(f[0]);
	 var sH = parseInt(s[0]);
	 var fM = parseInt(f[1]);
	 var sM = parseInt(s[1]);

	 var newH = fH + sH;
	 var newM = fM + sM;

	 if (newM > 59) {
		  newH += 1;
		  newM -= 60;
	 }

	 return newH.toString().padStart(2, '0') + ":" + newM.toString().padStart(2, '0');
}

function lockTextFields(trueFalse) {
	 $('input').each(function (i) {
		  if ($(this).attr('id') != "txtKomentar") {
				$(this).prop('disabled', trueFalse);
		  }
	 });
}

function calculateTotals() {
	 var newTotal = "00:00";
	 var newTotalRed = "00:00";
	 var newTotalPre = "00:00";

	 $('.zabiljezeno').each(function (i) {
		  newTotal = addHoursMinutes(newTotal, $(this).text().trim());
	 });

	 $('input').each(function (i) {
		  if ($(this).hasClass('radni')) {
				newTotalRed = addHoursMinutes(newTotalRed, $(this).val());
		  } else if ($(this).hasClass('prekovremeni')) {
				newTotalPre = addHoursMinutes(newTotalPre, $(this).val());
		  }
	 });

	 $('#txtTotal').html(newTotal);
	 $('#txtTotalRedovni').html(newTotalRed);
	 $('#txtTotalPrekovremeni').html(newTotalPre);

}

function calculateProjectTotals() {
	 var id, newTotal;

	 $('.zabiljezeno').each(function (i) {
		  id = $(this).attr('id').split('txtZab')[1];
		  var redovni = '#txtRadni' + id;
		  var prekovremeni = '#txtPrekovremeni' + id;

		  newTotal = addHoursMinutes($(redovni).val(), $(prekovremeni).val());

		  $(this).html(newTotal);
	 });
}

function getProjektZabiljezeno() {
	 var zapisi = new Array();

	 var id;

	 $('.zabiljezeno').each(function (i) {
		  id = $(this).attr('id').split('txtZab')[1];
		  var redovni = '#txtRadni' + id;
		  var prekovremeni = '#txtPrekovremeni' + id;

		  zapis = {
				ProjectId: parseInt(id),
				RedovniPrekovremeni: [
					 $(redovni).val(),
					 $(prekovremeni).val()
				]
		  };

		  zapisi[i] = zapis;

	 });

	 return zapisi;

}

function parseStringMinutesToFloat(str) {
	 var s = (str != '' && regex.test(str)) ? str.split(':') : ['00', '00'];

	 var h = parseInt(s[0]);
	 var m = parseInt(s[1]);

	 return (h * 60) + m;
}

function parseMinutesToString(minutes) {
	 var num = Math.floor(minutes);
	 var h = Math.floor(num / 60);
	 var m = num % 60;
	 return h.toString().padStart(2, '0') + ':' + m.toString().padStart(2, '0');
}

$('body').delegate("button[id|='btnDelete']", "click", function (e) {
	 e.preventDefault();
	 var btn = $(this);
	 var uspId = btn.attr('id').split('btnDelete-')[1];;

	 $.ajax({
		  url: "/Satnica/ObrisiUnosSatniceProjekta/" + uspId,
		  method: "POST",
		  success: function (res) {
				if (res != "error") {
					 btn.parent().parent().remove();
				}
		  }
	 });
});

$('body').delegate("button[id|='btnEdit']", "click", function (e) {
	 e.preventDefault();
	 var btn = $(this);
	 var uspId = btn.attr('id').split('btnEdit-')[1];;

	 var s = new Date();
	 s.setHours(0);
	 s.setMinutes(0);
	 s.setSeconds(0);

	 var endDate = new Date();
	 endDate.setHours(0);
	 endDate.setMinutes(0);
	 endDate.setSeconds(0);

	 var odId = "spTxt-Od-" + uspId;
	 var doId = "spTxt-Do-" + uspId;

	 var editId = "btnEdit-" + uspId;
	 document.getElementById(editId).disabled = true;

	 var odMin = parseStringMinutesToFloat(document.getElementById(odId).value);
	 var doMin = parseStringMinutesToFloat(document.getElementById(doId).value);

	 if (odMin < doMin) {
		  var startEnd = doMin - odMin;

		  var start = s.setMinutes(odMin);

		  var end = endDate.setMinutes(doMin);

		  var model = {
				IDSatnicaProjekta: uspId,
				SatnicaID: 0,
				ProjektID: $('#projId').val(),
				Start: new Date(start),
				End: new Date(end),
				TotalMin: startEnd
		  };

		  $.ajax({
				url: "/Satnica/UpdateUnosSatniceProjekta",
				method: "POST",
				data: JSON.stringify(model),
				contentType: 'application/json',
				success: function (res) {
					 //res[0] = id satnice projekta
					 //res[1] = projekt id
					 if (res != "error") {
						  alert("@UnosSati.Promjene_spremljene");

						  var odId = "spTxt-Od-" + res[0];
						  var doId = "spTxt-Do-" + res[0];

						  $('#txtTotalProjekta').html(
								addHoursMinutes(
									 $('#txtTotalProjekta').html(),
									 calculateDifference(
										  document.getElementById(odId).value,
										  document.getElementById(doId).value)));

						  var mainProjectTotal = "txtZab" + res[1];
						  document.getElementById(mainProjectTotal).innerHTML = $('#txtTotalProjekta').html().trim();
						  calculateTotals();
					 } else {
						  alert("@Common.Greska")
					 }
				}
		  });
	 } else {
		  alert("@Common.Krivi_unos");
	 }
});

$('body').delegate("input", "change", function (e) {
	 e.preventDefault();
	 var time;
	 var input = $(this);

	 if (input.attr('id').startsWith('spTxt')) {
		  var uspId = input.attr('id').split('-')[2];

		  var editId = "btnEdit-" + uspId;
		  document.getElementById(editId).disabled = false;

	 } else if (regex.test(input.val())) {
		  if (input.attr('id').startsWith('txtRadni')) {
				time =
					 addHoursMinutes(document.getElementById("txtTotalRedovni").innerHTML.trim(),	input.val());
				var h = parseInt(time.split(':')[0]);
				var m = parseInt(time.split(':')[1]);
				if ((h > 8) || (h == 8 && m != 0)) {
					 input.css('border', '3px solid red');
				} else {
					 document.getElementById("txtTotalRedovni").innerHTML = time;
					 calculateProjectTotals();
					 input.css('border', '1px solid #ced4da');
					 if (h == 8 && m == 0) {
						  $('#btnSpremi').prop('disabled', false);
					 }
				}
		  } else if ($(this).attr('id').startsWith('txtPrekovremeni')) {

		  time = addHoursMinutes(document.getElementById("txtTotalPrekovremeni").innerHTML.trim(), input.val());
		  var totalTime = addHoursMinutes(time, document.getElementById("txtTotalRedovni").innerHTML.trim());
		  var h = parseInt(totalTime.split(':')[0]);
		  var m = parseInt(totalTime.split(':')[1]);

				if ((h > 12) || (h == 12 && m != 0)) {
					 input.css('border', '3px solid red');
				} else {
					 document.getElementById("txtTotalPrekovremeni").innerHTML = addHoursMinutes(
						  document.getElementById("txtTotalPrekovremeni").innerHTML.trim(),
						  input.val());
					 calculateProjectTotals();
					 input.css('border', '1px solid #ced4da');
				}
		  }
	 } else {
		  input.css('border', '3px solid red');
	 }
});