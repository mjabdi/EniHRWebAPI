export class ChangeColumn
{
   colIndex :number;
   currentValue :string;
   newValue : string;
}

export class ValidationResponse
{
    rowIndex : number;
    isChanged : boolean;
    isNew : boolean;
    changedColumns : ChangeColumn[];
}

export class UpdateICTReport
{
    newRecords : number;
    updatedRecords : number;
}