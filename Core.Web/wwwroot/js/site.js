// Write your Javascript code.


$.ajaxSetup({ cache: true });

$(document).ajaxError(function (e, jqxhr, settings, exception) {
    console.log("ajaxError");
    console.log(jqxhr.status);
    if (jqxhr.status !== 500 && jqxhr.status !== 404) {
        //panelAlertLayer(jqxhr.status + '//' + jqxhr.responseText);
        window.location.reload(true);
    }
    else {
        execScript(jqxhr.responseText);
    }
});

// 뒤로가기 막기
$(document).keydown(function (e) {
    if (e.target.nodeName !== "INPUT" && e.target.nodeName !== "TEXTAREA") {
        if (e.keyCode === 8) {
            return false;
        }
    }
});

/*비동기호출용ajax*/
function ajaxSync(url, data, sucess, error, datatype) {
    if (typeof (datatype) === "undefined") {
        datatype = "json";
    }

    //ga('send', 'event', 'AJAX', url.split('/')[1], url.split('/')[2]);

    $.ajax({
        url: url,
        contentype: "charset=utf-8",
        dataType: datatype,
        data: data,
        cache: true,
        async: true,
        type: "POST",
        beforeSend: function () {
            //$.mobile.loading("show");
        },
        complete: function () {
            //$.mobile.loading('hide'); // This will hide AJAX spinner
        }
    }).done(sucess).fail(error);
}


$(document).keydown(function (e) {
    if (e.target.nodeName !== "INPUT" && e.target.nodeName !== "TEXTAREA") {
        if (e.keyCode === 8) {
            return false;
        }
    }
});


function Language(ctrl) {

    var obj = this;
    
    obj.btnSearch = $(ctrl.btnSearch);
    obj.txtKeyword = $(ctrl.txtKeyword);
    obj.selServiceList = $(ctrl.selServiceList);
    obj.btnCreateLink = $(ctrl.btnCreateLink);
    obj.languageContainer = jQuery(ctrl.languageContainer);

    obj.currentPage = 1;
    obj.linePerPage = 100;

    obj.serviceList = "";

    obj.init = function () {
        obj.getServiceList();
    };

    obj.reset = function () {
        obj.currentPage = 1;
        obj.linePerPage = 100;

        obj.languageListLayer.hide();
        obj.languageCreateLayer.hide();
    };

    obj.selServiceList.change(function () {
        obj.getLanguageList();
    });

    obj.btnSearch.click(function () {
        obj.getLanguageList();
    });

    obj.txtKeyword.autocomplete({
        delay: 500,
        source: function (request, response) {
            obj.getLanguageList();
        }
    });

    obj.getServiceList = function () {

        var requestData = {
        };

        var fnSuccess = function (data) {
            if (data.rows.length === 0) {
                return null;
            }

            var iterHtml = "";
            iterHtml += "<option value='' selected='selected'>선 택</option>";

            obj.serviceList = "{";
            for (var i = 0; i < data.rows.length; i++) {
                iterHtml += "<option value=\"" + data.rows[i].serviceNo + "\">" + data.rows[i].serviceName + "</option>";

                obj.serviceList += (i !== data.rows.length - 1) ? "\"" + data.rows[i].serviceNo + "\":\"" + data.rows[i].serviceName + "\","
                    : "\"" + data.rows[i].serviceNo + "\":\"" + data.rows[i].serviceName + "\"";
            }
            obj.serviceList += "}";

            obj.selServiceList.empty().append(iterHtml).trigger('create');

            if (data.error !== undefined) {
                panelAlertLayer(data.error, "error");
            }
            //else {
            //    if (data.resultCode === "-1") {
            //    }
            //    else {
            //    }
            //}
        };

        var fnError = function () {

            alert('오류 발생', "error");
        };

        ajaxSync("/Language/GetServiceList", requestData, fnSuccess, fnError);
    };

    obj.setGridParam = function () {

        var requestData = {
            ServiceNo: obj.selServiceList.val(),
            KeyName: obj.txtKeyword.val()
        };

        obj.languageContainer.jqGrid('setGridParam', { page: 1, datatype: 'json', postData: requestData }).trigger('reloadGrid');
    }

    obj.getLanguageList = function () {

        var lastsel;

        var requestData = {
            ServiceNo: obj.selServiceList.val(),
            KeyName: obj.txtKeyword.val()
        };

        obj.languageContainer.jqGrid('setGridParam', { page: 1, datatype: 'json', postData: requestData }).trigger('reloadGrid');

        obj.languageContainer.jqGrid({
            url: '/Language/GetLanguageList',
            postData: requestData,
            mtype: 'POST',
            datatype: "json",
            colNames: ['No', 'ServiceNo', 'key', 'en', 'zh_CN', 'zh_TW', 'ja', 'ru'],
            colModel: [
                { name: 'languageNo', index: 'LanguageNo', align: "center", width: 40, editable: true, editrules: { edithidden: false }, hidedlg: true, hidden: true },
                { name: 'serviceNo', index: 'ServiceNo', align: "center", width: 40, editable: true, edittype: "select", editoptions: { value: JSON.parse(obj.serviceList) }, hidedlg: false, hidden: false },
                { name: 'key', index: 'Key', align: "center", width: 100, editable: true, sortable: true },
                { name: 'en', index: 'en', width: 150, align: "left", editable: true },
                { name: 'zh_CN', index: 'zh_CN', width: 150, align: "left", editable: true },
                { name: 'zh_TW', index: 'zh_TW', width: 150, align: "left", editable: true },
                { name: 'ja', index: 'ja', width: 150, align: "left", editable: true },
                { name: 'ru', index: 'ru', width: 150, align: "left", editable: true }
            ],
            rowNum: obj.linePerPage,
            autowidth: true,
            height: 800,
            rowList: [obj.linePerPage, obj.linePerPage * 2, obj.linePerPage * 3],
            pager: '#languagePager',
            sortname: 'Key',
            viewrecords: true,
            sortorder: "desc",
            multiselect: true,
            emptyrecords: "Empty data.",
            onSelectRow: function (id) {
                if (id && id !== lastsel) {
                    var rowData = obj.languageContainer.getRowData(id);
                    obj.languageContainer.jqGrid('restoreRow', lastsel);
                    obj.languageContainer.jqGrid('editRow', id, true);
                    lastsel = id;
                }
            },
            ondblClickRow: function (rowid, iRow, iCol, e) {
                $(this).jqGrid('editGridRow', rowid);
            },
            loadComplete: function (data) {
                //if (data.rows.length > 0) {
                //}
                //else {
                //}

                if (data.error !== undefined) {
                    alert(data.error, "error");
                }
                //else {
                    
                //}
            },
            jsonReader: {
                root: "rows",
                repeatitems: false,
                page: "page",
                total: "total",
                records: "records"
            },
            editurl: "/Language/Manage",
            caption: "LanguageSet"
        });

        $.extend($.jgrid.edit, {
            bSubmit: "Save and Close",
            bCancel: "Cancel",
            width: 650,
            recreateForm: true,
            beforeShowForm: function () {
                $('<a href="#">Clear<span class="ui-icon ui-icon-document-b"></span></a>')
                    .click(function () {
                        $(".ui-jqdialog input").val("");
                        $(this).closest('.ui-dialog-content').dialog('close');
                    }).addClass("fm-button ui-state-default ui-corner-all fm-button-icon-left")
                      .prependTo("#Act_Buttons>td.EditButton");
            }
        });

        $.extend($.jgrid.del, {
            bSubmit: "Delete",
            bCancel: "Cancel",
            recreateForm: true,
            onclickSubmit: function (options, ids) {
                var $self = $(this);
                var selectedrows = ids.split(",");
                var selectedData = [];

                for (var i = 0; i < selectedrows.length; i++) {
                    // fill array selectedData with the data from selected rows
                    selectedData.push($self.jqGrid("getRowData", selectedrows[i]).languageNo);
                }

                // the returned data will be combined with the default data
                // posted by delGridRow
                return {
                    languageNoList: JSON.stringify(selectedData)
                }
            }
        });

        obj.languageContainer.jqGrid('navGrid', "#languagePager", {
            edit: true, add: true, del: true, search: false
        });
    }
}
