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
    Elements: SelectElement[];
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

        if (this.column.Type === ColumnType.text || this.column.Type === ColumnType.date || this.column.Type === ColumnType.number) {
            return this.control.value;
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

        if (this.column.Type === ColumnType.text || this.column.Type === ColumnType.date || this.column.Type === ColumnType.number) {
            var input = document.createElement("input");
            input.type = String(ColumnType[this.column.Type]);
            input.value = this.value;
            cell.appendChild(input);
            this.control = input;            
        }  

        if (this.column.Type === ColumnType.list) {
            var select = document.createElement("select");
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
            cell.appendChild(checkbox);
            this.control = checkbox;
        }

        return cell;
    }
}

class RowTable {
    public Cells = new Array<CellTable>();
    public Row: HTMLTableRowElement;

    public createRow(table: HTMLTableElement) {
        var rowElement = document.createElement("tr");
        for (var i = 0; i < this.Cells.length; i++) {
            var cellElement = this.Cells[i].createInput();            
            rowElement.appendChild(cellElement);            
        }
        this.Row = rowElement;
        table.appendChild(rowElement);
    }

    public deleteRow(table: HTMLTableElement) {
        table.removeChild(this.Row);
    }
}

class HelloTable {
    public Columns: ColumnTable[];
    public Rows = new Array<RowTable>();
    private _data: any;
    private _table: HTMLTableElement;

    constructor(tagTableId: string) {
        this._table = <HTMLTableElement>(document.getElementById(tagTableId));
        this.Columns = new Array();
    }

    get jsonData() {
        return null;
    }

    set jsonData(value: any) {
        
    }

    get data() {
        var result = [];

        for (var r = 0; r < this.Rows.length; r++) {
            var row = {};
            for (var c = 0; c < this.Rows[r].Cells.length; c++) {
                row[this.Rows[r].Cells[c].column.FieldName] = this.Rows[r].Cells[c].Value;
            }
            result.push(row);
        }
        return <any>result;
    }

    set data(jsonContent: string) {
        this._data = jsonContent;
        this.clean();

        var header = document.createElement("thead");
        
        for (var i = 0; i < this.Columns.length; i++) {
            var hCell = document.createElement("th");
            hCell.innerHTML = this.Columns[i].Caption;
            header.appendChild(hCell);            
        }
        this._table.appendChild(header);

        var d = JSON.parse(jsonContent);

        for (var i = 0; i < d.length; i++) {
            var row = new RowTable();
            for (var j = 0; j < this.Columns.length; j++) {
                var cell = new CellTable(this.Columns[j], d[i][this.Columns[j].FieldName]);
                row.Cells.push(cell);              
            }
            this.Rows.push(row);
            row.createRow(this._table);
        }
    }

    public insertRow() {
        var row = new RowTable();
        for (var i = 0; i < this.Columns.length; i++) {
            var cell = new CellTable(this.Columns[i], "");
            row.Cells.push(cell);
        }
        this.Rows.push(row);
        row.createRow(this._table);
    }

    public deleteRow(row:RowTable) {
        row.deleteRow(this._table);
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

//var c = new ColumnTable();
//c.Type = ColumnType.list;
//c.Elements.push(new SelectElement(1, "item 1"));
//c.Elements.push(new SelectElement(2, "item 2"));
