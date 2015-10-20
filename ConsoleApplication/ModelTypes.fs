module ModelTypes

type Model = DrivenCell option list
and DrivenCell =
    | BlankCell
    | NumberCell of decimal
    | FormulaCell of Formula
and Formula =
    { Expr: Expression; CachedValue: Value option; Model: Model; State: CellState }
and CellState =
    | Dirty
    | Enqueued
    | Calculating
    | UpToDate
and Expression =
    | NumberConstant of decimal
    | Error of Error
    | CellRef of Model * RelRef
    | Function of Func * Expression list // the Expression list is the list of expressions given as arguments to the Function
and Value =
    | NumberValue of decimal
    | ErrorValue of Error
    | FunctionValue of Model // ?
and RelRef =
    { MonthAbsolute: bool; Month: int; Dimensions: string[] }
and Error = 
    { Message: string }
and Func =
    { IsVolitile: bool; Applier: Expression list -> Value; Fixity: int } // the Expression[] is the list of expressions given as arguments to the Function