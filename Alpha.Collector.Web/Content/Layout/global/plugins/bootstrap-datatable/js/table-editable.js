var TableEditable = function () {

    var handleTable = function () {

        function restoreRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                oTable.fnUpdate(aData[i], nRow, i, false);
            }
            oTable.fnDraw();
        }

        function editRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            
            jqTds[0].innerHTML = '<input type="checkbox" class="form-control input-small" ' + (aData[0] == "已勾选" ? "checked" : "") + '>';
            jqTds[1].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[1] + '">';
            jqTds[2].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[2] + '">';
            jqTds[3].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[3] + '">';
            jqTds[4].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[4] + '">';
            jqTds[5].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[5] + '">';
            jqTds[6].innerHTML = '<a class="edit" href="">保存</a>';
            jqTds[7].innerHTML = '<a class="cancel" href="">取消</a>';
           
        }

        function saveRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            
            var checkbox = jqInputs[0];
            var ischecked = $(checkbox).is(':checked');
            var regx = new RegExp("^[0-9]+(\.[0-9]{1,2})?$");
            var intRegx = /^[0-9]+$/;
            
            if (!regx.test(jqInputs[1].value)) {
                App.Alert("面值只能输入整数或最多两位小数");
                return false;
            }
            if (!regx.test(jqInputs[2].value)) {
                App.Alert("闪惠价只能输入整数或最多两位小数");
                return false;
            }
            if (!regx.test(jqInputs[3].value)) {
                App.Alert("录入协议价只能输入整数或最多两位小数");
                return false;
            }
            if (!regx.test(jqInputs[4].value)) {
                App.Alert("原价只能输入整数或最多两位小数");
                return false;
            }
            
            if (!intRegx.test(jqInputs[5].value)) {
                App.Alert("库存只能输入整数");
                return false;
            }
            oTable.fnUpdate(ischecked ? "已勾选" : "", nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oTable.fnUpdate('<a class="edit" href="">修改</a>', nRow, 6, false);
            oTable.fnUpdate('<a class="delete" href="">删除</a>', nRow, 7, false);
            oTable.fnDraw();
            return true;
        }

        function cancelEditRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oTable.fnUpdate('<a class="edit" href="">修改</a>', nRow, 6, false);
            oTable.fnDraw();
        }

        var table = $('#sample_editable_1');

        var oTable = table.dataTable({

            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "lengthMenu": [
                [5, 15, 20, -1],
                [5, 15, 20, "All"] // change per page values here
            ],

            // Or you can use remote translation file
            //"language": {
            //   url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
            //},

            // set the initial value
            "pageLength": 10000000,

            "language": {
                "lengthMenu": ""
            },
            "columnDefs": [{ // set default column settings
                'orderable': false,
                'targets': [0]
            }, {
                "searchable": false,
                "targets": [0]
            }],
            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });



        var nEditing = null;
        var nNew = false;

        $('#btnAddMobileSettings').click(function (e) {
            e.preventDefault();
            
            if (nNew && nEditing) {
                if (confirm("当前还有未保存的数据，是否立即保存 ?")) {
                    return saveRow(oTable, nEditing); // save
                    $(nEditing).find("td:first").html("");
                    nEditing = null;
                    nNew = false;

                } else {
                    oTable.fnDeleteRow(nEditing); // cancel
                    nEditing = null;
                    nNew = false;

                    return;
                }
            }
            var aiNew = oTable.fnAddData(['', '', '', '', '', '', '','']);
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            nEditing = nRow;
            nNew = true;
        });

        table.delegate('.delete', 'click', function (e) {
            e.preventDefault();

            if (confirm("确认移除当前数据?") == false) {
                return;
            }

            var nRow = $(this).parents('tr')[0];
            oTable.fnDeleteRow(nRow);
            //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();
            if (nNew) {
                oTable.fnDeleteRow(nEditing);
                nEditing = null;
                nNew = false;
            } else {
                restoreRow(oTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.edit', function (e) {
            e.preventDefault();
            
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTable, nEditing);
                editRow(oTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "保存") {
                /* Editing this row and want to save it */
                if (saveRow(oTable, nRow)) {
                    nEditing = null;
                }
                //saveRow(oTable, nEditing);

                //alert("Updated! Do not forget to do some ajax to sync with backend :)");
            } else {
                /* No edit in progress - let's start one */
                editRow(oTable, nRow);
                nEditing = nRow;
            }
        });
    }

    return {

        //main function to initiate the module
        init: function () {
            handleTable();
        }

    };

}();