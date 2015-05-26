enum ColumnType { text, date, number, list, boolean }

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

class Cell {
    private _value: any;
    get Value() {
        return this._value;
    }
    set Value(val: any) {
        this._value = val;
    }
}

class RowTable {
    Cells: Cell[];
}

class HelloTable {
    Columns: ColumnTable[];
    Rows: RowTable[];
    private _data: string;
    private _table: HTMLElement;
    constructor(tagTableId: string) {
        this._table = document.getElementById("tagTableId");
    }
    get Data() {
        return this._data;
    }
    set Data(JsonContent: string) {
        this._data = JsonContent;
    }

    private AddRow() {
    }

    private DeleteRow(lineNumber: number) {
    }

    private CreateInput(column: ColumnTable, value: string) {
    }

    private GetInputValue(lineNumber: number, column: ColumnTable): string {
        return "";
    }
}

var c = new ColumnTable();
c.Type = ColumnType.list;
c.Elements.push(new SelectElement(1, "item 1"));
c.Elements.push(new SelectElement(2, "item 2"));

var t = new HelloTable("tabTelefones");