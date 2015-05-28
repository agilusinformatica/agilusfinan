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
    private _column: ColumnTable;
    private _value: any;
    
    constructor(column: ColumnTable, value: any) {
        this._column = column;
        this._value = value;
    }

    get Value() {
        return this._value;
    }
    set Value(val: any) {
        this._value = val;
    }

    public CreateInput(): HTMLTableCellElement {  
        var cell = document.createElement("td");
        if (this._column.Type === ColumnType.text) {
            var input = document.createElement("input");
            input.type = String(this._column.Type);
            input.value = this._value;
            cell.appendChild(input);
        }              
        return cell;
    }
}

class RowTable {
    private _cells = new Array<CellTable>();

    set Cells(cell: CellTable) {
        this._cells.push(cell);
    }

    get Cells() {
        return null;
    }

    public CreateRow(): HTMLTableRowElement {
        var rowElement = document.createElement("tr");
        for (var i = 0; i < this._cells.length; i++) {
            var cellElement = this._cells[i].CreateInput();
            rowElement.appendChild(cellElement);
        }
        return rowElement;
    }
}

class HelloTable {
    public Columns: ColumnTable[];
    public Rows = new Array<RowTable>();
    private _data: string;
    private _table: HTMLTableElement;
    constructor(tagTableId: string) {
        this._table = <HTMLTableElement>(document.getElementById(tagTableId));
        this.Columns = new Array();
    }
    get Data() {
        return this._data;
    }
    set Data(JsonContent: string) {
        this._data = JsonContent;
        this.Clean();

        var header = document.createElement("thead");
        
        for (var i:number = 0; i < this.Columns.length; i++) {
            var hCell = document.createElement("th");
            hCell.innerHTML = this.Columns[i].Caption;
            header.appendChild(hCell);            
        }
        this._table.appendChild(header);

        var d = JSON.parse(JsonContent);

        for (var i: number = 0; i < d.length; i++) {
            //var row = this.AddRow();
            var row = new RowTable();
            for (var j: number = 0; j < this.Columns.length; j++) {
                var cell = new CellTable(this.Columns[j], d[i][this.Columns[j].FieldName]);
                row.Cells = cell;              
                //this.Rows.push(cell);
                //row.appendChild(cell.CreateInput(this.Columns[j], d[i][this.Columns[j].FieldName]));
            }
            this._table.appendChild(row.CreateRow());
        }
    }

    private AddRow(): HTMLTableRowElement {
        var row = document.createElement("tr");
        return row;
    }

    private Clean() {
        var rowsLength = this._table.rows.length;
        for (var i: number = rowsLength - 1; i >= 0; i--) {
            this._table.deleteRow(i);    
        }        
    }

    private DeleteRow(lineNumber: number) {
    }

    private GetInputValue(lineNumber: number, column: ColumnTable): string {
        return "";
    }
}

//var c = new ColumnTable();
//c.Type = ColumnType.list;
//c.Elements.push(new SelectElement(1, "item 1"));
//c.Elements.push(new SelectElement(2, "item 2"));
