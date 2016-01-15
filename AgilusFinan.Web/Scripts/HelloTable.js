var ColumnType;
(function (ColumnType) {
    ColumnType[ColumnType["text"] = 0] = "text";
    ColumnType[ColumnType["date"] = 1] = "date";
    ColumnType[ColumnType["number"] = 2] = "number";
    ColumnType[ColumnType["list"] = 3] = "list";
    ColumnType[ColumnType["boolean"] = 4] = "boolean";
    ColumnType[ColumnType["hidden"] = 5] = "hidden";
})(ColumnType || (ColumnType = {}));

var SelectElement = (function () {
    function SelectElement(_key, _value) {
        this.key = _key;
        this.value = _value;
    }
    return SelectElement;
})();

var ColumnTable = (function () {
    function ColumnTable(_caption, _fieldName, _type, _elements, _mask, _cssClass) {
        this.Elements = [];
        this.Caption = _caption;
        this.FieldName = _fieldName;
        this.Type = _type;
        this.Mask = _mask;
        this.CssClass = _cssClass;
        if (_elements) {
            for (var i = 0; i < _elements.length; i++) {
                this.Elements.push(_elements[i]);
            }
        }
    }
    return ColumnTable;
})();

var CellTable = (function () {
    function CellTable(column, value) {
        this.column = column;
        this.value = value;
    }
    Object.defineProperty(CellTable.prototype, "Value", {
        get: function () {
            if (this.column.Type === 0 /* text */ || this.column.Type === 1 /* date */ || this.column.Type === 2 /* number */ || this.column.Type === 5 /* hidden */) {
                if (this.control.value.match(/\d+,\d+/g)) {
                    var valor = this.control.value.replace(".", "");
                    return valor.replace(",", ".");
                } else {
                    return this.control.value;
                }
            }

            if (this.column.Type === 3 /* list */) {
                for (var i = 0; i < this.control.options.length; i++) {
                    if (this.control.options[i].selected) {
                        return this.control.options[i].value;
                    }
                }
            }

            if (this.column.Type === 4 /* boolean */) {
                return this.control.checked;
            }
        },
        set: function (val) {
            this.value = val;
        },
        enumerable: true,
        configurable: true
    });


    CellTable.prototype.createInput = function () {
        var cell = document.createElement("td");

        if (this.column.Type === 0 /* text */ || this.column.Type === 2 /* number */ || this.column.Type === 5 /* hidden */) {
            var input = document.createElement("input");
            input.type = String(ColumnType[this.column.Type]);
            input.value = this.value;
            if (this.column.CssClass)
                input.className = this.column.CssClass;
            cell.appendChild(input);
            this.control = input;
        }

        if (this.column.Type === 1 /* date */) {
            var input = document.createElement("input");
            input.type = String(ColumnType[this.column.Type]);

            if (this.value) {
                var date = new Date(Number(this.value.substring(6, this.value.length - 2)));
                input.value = date.toISOString().substring(0, 10);
                console.log(input.value);
            }

            if (this.column.CssClass)
                input.className = this.column.CssClass;
            cell.appendChild(input);
            this.control = input;
        }

        if (this.column.Type === 3 /* list */) {
            var select = document.createElement("select");
            if (this.column.CssClass)
                select.className = this.column.CssClass;
            var optionBlank = document.createElement("option");
            select.appendChild(optionBlank);
            for (var i = 0; i < this.column.Elements.length; i++) {
                var option = document.createElement("option");
                var text = document.createTextNode(this.column.Elements[i].value);
                option.value = String(this.column.Elements[i].key);
                option.selected = this.value == this.column.Elements[i].key;
                option.appendChild(text);
                select.add(option);
                cell.appendChild(select);
            }
            this.control = select;
        }

        if (this.column.Type === 4 /* boolean */) {
            var checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.checked = this.value;
            if (this.column.CssClass)
                checkbox.className = this.column.CssClass;
            cell.appendChild(checkbox);
            this.control = checkbox;
        }

        if (this.column.Mask) {
            Utils.createMask(this.control, this.column.Mask);
        }
        ;

        return cell;
    };
    return CellTable;
})();

var RowTable = (function () {
    function RowTable(helloTable) {
        this.Cells = new Array();
        this._helloTable = helloTable;
    }
    RowTable.prototype.createRow = function (table) {
        var _this = this;
        var rowElement = document.createElement("tr");
        for (var i = 0; i < this.Cells.length; i++) {
            var cellElement = this.Cells[i].createInput();
            rowElement.appendChild(cellElement);
        }

        var cellButton = document.createElement("td");
        var text = document.createTextNode("Apagar");

        //var image = document.createElement("img");
        //image.src = "/Content/Images/close.png";
        var deleteButton = document.createElement("button");
        deleteButton.onclick = function (e) {
            e.preventDefault();
            _this.Row.classList.add("fadeout");
            var rows = _this.Row.childNodes;
            console.log(rows);
            for (var i = 0; i < rows.length; i++) {
                var cell = rows.item(i);
                cell.classList.add("fadeout");
            }

            setTimeout(function () {
                return _this.deleteRow(table);
            }, 400);
        };

        //deleteButton.appendChild(image);
        deleteButton.appendChild(text);
        cellButton.appendChild(deleteButton);
        rowElement.appendChild(cellButton);

        this.Row = rowElement;
        table.appendChild(rowElement);
    };

    RowTable.prototype.deleteRow = function (table) {
        table.removeChild(this.Row);
        this._helloTable.removeRow(this);
    };
    return RowTable;
})();

var HelloTable = (function () {
    function HelloTable(tagTableId, tagInsertButtonId) {
        var _this = this;
        this.Rows = new Array();
        this._table = (document.getElementById(tagTableId));
        this.Columns = new Array();

        if (tagInsertButtonId) {
            var button = document.getElementById(tagInsertButtonId);

            button.onclick = function (e) {
                e.preventDefault();
                _this.insertRow();
            };
        }
    }
    Object.defineProperty(HelloTable.prototype, "jsonData", {
        get: function () {
            return JSON.stringify(this.data);
        },
        set: function (jsonContent) {
            this.data = JSON.parse(jsonContent);
        },
        enumerable: true,
        configurable: true
    });


    Object.defineProperty(HelloTable.prototype, "data", {
        get: function () {
            var result = [];

            for (var r = 0; r < this.Rows.length; r++) {
                var row = {};
                for (var c = 0; c < this.Rows[r].Cells.length; c++) {
                    row[this.Rows[r].Cells[c].column.FieldName] = Utils.convertFormatDate(this.Rows[r].Cells[c].Value);
                    console.log(Utils.convertFormatDate(this.Rows[r].Cells[c].Value));
                }
                result.push(row);
            }
            return result;
        },
        set: function (value) {
            this.clean();

            var header = document.createElement("thead");

            for (var i = 0; i < this.Columns.length; i++) {
                var hCell = document.createElement("th");
                hCell.innerHTML = this.Columns[i].Caption;
                header.appendChild(hCell);
            }
            this._table.appendChild(header);

            for (var i = 0; i < value.length; i++) {
                var row = new RowTable(this);
                for (var j = 0; j < this.Columns.length; j++) {
                    var cell = new CellTable(this.Columns[j], value[i][this.Columns[j].FieldName]);
                    row.Cells.push(cell);
                }
                this.Rows.push(row);
                row.createRow(this._table);
            }
        },
        enumerable: true,
        configurable: true
    });


    HelloTable.prototype.insertRow = function () {
        var row = new RowTable(this);
        for (var i = 0; i < this.Columns.length; i++) {
            var cell = new CellTable(this.Columns[i], "");
            row.Cells.push(cell);
        }
        this.Rows.push(row);
        row.createRow(this._table);
    };

    HelloTable.prototype.removeRow = function (row) {
        var index = this.Rows.indexOf(row);
        this.Rows.splice(index, 1);
    };

    HelloTable.prototype.clean = function () {
        var rowsLength = this._table.rows.length;
        for (var i = rowsLength - 1; i >= 0; i--) {
            this._table.deleteRow(i);
        }
    };
    return HelloTable;
})();
//# sourceMappingURL=HelloTable.js.map
