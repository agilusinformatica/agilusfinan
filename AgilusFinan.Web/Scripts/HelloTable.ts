enum ColumnType { text, date, number, list, boolean, hidden }

class SelectElement {
    key: number;
    value: string;
    constructor(_key: number, _value: string) {
        this.key = _key;
        this.value = _value;
    }
}

class ColumnTable {
    Caption: string;
    FieldName: string;
    Type: ColumnType;
    Mask: string;
    CssClass: string;
    Elements: SelectElement[] = [];

    constructor(_caption: string, _fieldName: string, _type: ColumnType, _elements?: SelectElement[], _mask?: string, _cssClass?: string) {
        this.Caption = _caption;
        this.FieldName = _fieldName;
        this.Type = _type;
        this.Mask = _mask;
        this.CssClass = _cssClass;
        if(_elements) {
            for (var i = 0; i < _elements.length; i++) {
                this.Elements.push(_elements[i]);
            }
        }
    }
} 

class CellTable {
    public column: ColumnTable;
    private value: any;
    private selected: number;
    private control;

    constructor(column: ColumnTable, value: any) {
        this.column = column;
        this.value = value;
    }

    get Value() {

        if (this.column.Type === ColumnType.text || this.column.Type === ColumnType.date || this.column.Type === ColumnType.number || this.column.Type === ColumnType.hidden) {
            if (this.control.value.match(/\d+,\d+/g)) {
                var valor = this.control.value.replace(".", "");                                           
                return valor.replace(",", ".");
            } else {
                return this.control.value;    
            }
            
        }

        if (this.column.Type === ColumnType.list) {
            for (var i = 0; i < this.control.options.length; i++) {
                if (this.control.options[i].selected) {
                    return this.control.options[i].value;
                }
            }
        }

        if (this.column.Type === ColumnType.boolean) {
            return this.control.checked;
        }

    }

    set Value(val: any) {
        this.value = val;
    }

    public createInput(): HTMLTableCellElement {  
        var cell = document.createElement("td");

        if (this.column.Type === ColumnType.text || this.column.Type === ColumnType.number || this.column.Type === ColumnType.hidden) {
            var input = document.createElement("input");
            input.type = String(ColumnType[this.column.Type]);
            input.value = this.value;
            if (this.column.CssClass)
                input.className = this.column.CssClass;
            cell.appendChild(input);
            this.control = input;            
        }  

        if (this.column.Type === ColumnType.date) {
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

        if (this.column.Type === ColumnType.list) {
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

        if (this.column.Type === ColumnType.boolean) {
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
        };

        return cell;
    }
}

class RowTable {
    public Cells = new Array<CellTable>();
    public Row: HTMLTableRowElement;
    private _helloTable: HelloTable;

    constructor(helloTable: HelloTable) {
        this._helloTable = helloTable;
    }

    public createRow(table: HTMLTableElement) {
        var rowElement = document.createElement("tr");
        for (var i = 0; i < this.Cells.length; i++) {
            var cellElement = this.Cells[i].createInput();            
            rowElement.appendChild(cellElement);            
        }

        var cellButton = document.createElement("td");
        var image = document.createElement("img");
        image.src = "/Content/Images/close.png";
        var deleteButton = document.createElement("button");
        deleteButton.onclick = e => {
            e.preventDefault();
            this.Row.classList.add("fadeout");
            var rows = this.Row.childNodes;
            console.log(rows);
            for (var i = 0; i < rows.length;i++) {
                var cell = <any> rows.item(i);
                cell.classList.add("fadeout");
            }

            setTimeout(() => this.deleteRow(table), 400);
        };
        
        deleteButton.appendChild(image);
        cellButton.appendChild(deleteButton);        
        rowElement.appendChild(cellButton);

        this.Row = rowElement;
        table.appendChild(rowElement);
    }

    public deleteRow(table: HTMLTableElement) {
        table.removeChild(this.Row);
        this._helloTable.removeRow(this);

    }
}

class HelloTable {
    public Columns: ColumnTable[];
    public Rows = new Array<RowTable>();
    private _table: HTMLTableElement;

    constructor(tagTableId: string, tagInsertButtonId?: string) {
        this._table = <HTMLTableElement>(document.getElementById(tagTableId));
        this.Columns = new Array();

        if (tagInsertButtonId) {
            var button = document.getElementById(tagInsertButtonId);

            button.onclick = e => {
                e.preventDefault();
                this.insertRow();
            }
        }
    }

    get jsonData() {
        return JSON.stringify(this.data);
    }

    set jsonData(jsonContent: string) {
        this.data = JSON.parse(jsonContent);
    }

    get data() {
        var result = [];

        for (var r = 0; r < this.Rows.length; r++) {
            var row = {};
            for (var c = 0; c < this.Rows[r].Cells.length; c++) {
                row[this.Rows[r].Cells[c].column.FieldName] = Utils.convertFormatDate(this.Rows[r].Cells[c].Value);
                console.log(Utils.convertFormatDate(this.Rows[r].Cells[c].Value));
            }
            result.push(row);
        }
        return <any>result;
    }

    set data(value: any) {
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
    }

    public insertRow() {
        var row = new RowTable(this);
        for (var i = 0; i < this.Columns.length; i++) {
            var cell = new CellTable(this.Columns[i], "");
            row.Cells.push(cell);
        }
        this.Rows.push(row);
        row.createRow(this._table);
    }

    public removeRow(row:RowTable) {
        var index = this.Rows.indexOf(row);
        this.Rows.splice(index, 1);
    }

    private clean() {
        var rowsLength = this._table.rows.length;
        for (var i = rowsLength - 1; i >= 0; i--) {
            this._table.deleteRow(i);    
        }        
    }
}