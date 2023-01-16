namespace BlogProject.Services.Impl;

public class RandomService : IRandomService
{
    private readonly Random _rnd;

    public RandomService()
    {
        _rnd = new Random();
    }
    
    private string[] userFirstNames =
        "Abraham, Addison, Adrian, Albert, Alec, Alfred, Alvin, Andrew, Andy, Archibald, Archie, Arlo, Arthur, Arthur, Austen, Barnabe, Bartholomew, Bertram, Bramwell, Byam, Cardew, Chad, Chance, Colin, Coloman, Curtis, Cuthbert, Daniel, Darryl, David, Dickon, Donald, Dougie, Douglas, Earl, Ebenezer, Edgar, Edmund, Edward, Edwin, Elliot, Emil, Floyd, Franklin, Frederick, Gabriel, Galton, Gareth, George, Gerard, Gilbert, Gorden, Gordon, Graham, Grant, Henry, Hervey, Hudson, Hugh, Ian, Jack, Jaime, James, Jason, Jeffrey, Joey, John, Jolyon, Jonas, Joseph, Joshua, Julian, Justin, Kurt, Lanny, Larry, Laurence, Lawton, Lester, Malcolm, Marcus, Mark, Marshall, Martin, Marvin, Matt, Maximilian, Michael, Miles, Murray, Myron, Nate, Nathan, Neil, Nicholas, Nicolas, Norman, Oliver, Oscar, Osric, Owen, Patrick, Paul, Peleg, Philip, Phillipps, Raymond, Reginald, Rhys, Richard, Robert, Roderick, Rodger, Roger, Ronald, Rowland, Rufus, Russell, Sebastian, Shahaf, Simon, Stephen, Swaine, Thomas, Tobias, Travis, Victor, Vincent, Vincent, Vivian, Wayne, Wilfred, William, Winston, Zadoc"
            .Split(',').Select(x => x.Trim()).ToArray();

    private string[] userLastNames =
        "SMITH,BROWN,WILSON,ROBERTSON,CAMPBELL,STEWART,THOMSON,ANDERSON,SCOTT,MACDONALD,REID,MURRAY,CLARK,TAYLOR,ROSS,YOUNG,PATERSON,WATSON,MITCHELL,FRASER,MORRISON,WALKER,MCDONALD,GRAHAM,MILLER,JOHNSTON,HENDERSON,CAMERON,DUNCAN,GRAY,KERR,HAMILTON,HUNTER,DAVIDSON,FERGUSON,BELL,MACKENZIE,MARTIN,SIMPSON,GRANT,ALLAN,KELLY,MACLEOD,BLACK,MACKAY,WALLACE,MCLEAN,KENNEDY,GIBSON,RUSSELL,MARSHALL,GORDON,BURNS,STEVENSON,MILNE,CRAIG,WOOD,WRIGHT,MUNRO,JOHNSTONE,RITCHIE,SINCLAIR,WATT,MCKENZIE,MUIR,MURPHY,SUTHERLAND,MCMILLAN,WHITE,MCKAY,MILLAR,HUGHES,CRAWFORD,WILLIAMSON,DOCHERTY,MACLEAN,FLEMING,CUNNINGHAM,DICKSON,BOYLE,DOUGLAS,MCINTOSH,BRUCE,SHAW,MCGREGOR,LINDSAY,JAMIESON,HAY,CHRISTIE,BOYD,AITKEN,RAE,HILL,MCCALLUM,ALEXANDER,MCINTYRE,CURRIE,RAMSAY,MACKIE,WEIR,JONES,CAIRNS,WHYTE,MCLAUGHLIN,JACKSON,FINDLAY,FORBES,KING,DONALDSON,HUTCHISON,MCCULLOCH,MCLEOD,MCFARLANE,NICOL,BUCHANAN,PATON,MOORE,DUFFY,REILLY,RENNIE,TAIT,IRVINE,O'NEILL,THOMPSON,GREEN,MCEWAN,QUINN,HENDRY,BAIN,BEATTIE,CHALMERS,HALL,HOGG,WILLIAMS,STRACHAN,TURNER,LOGAN,COOK,ARMSTRONG,BUCHAN,COWAN,GALLAGHER,BARCLAY,WELSH,BARR,GALLACHER,GILMOUR,MURDOCH,BLAIR,FORSYTH,ADAMS,COOPER,DONNELLY,DICK,NELSON,O'DONNELL,WARD,JACK,MACPHERSON,DONALD,BAIRD,MCLAREN,PARK,DRUMMOND,INNES,GILLESPIE,LAWSON,DUNN,MAXWELL,MCPHERSON,COLLINS,SPENCE,HIGGINS,ROBERTS,DUFF,MCGOWAN,MCGUIRE,MOFFAT,MORGAN,BAXTER,MACFARLANE,MCBRIDE,MCDOUGALL,INGLIS,RICHARDSON,STEPHEN,SHARP,MACKINNON,MCALLISTER,GILLIES,LAIRD,RODGER,GREIG,LAING,MORTON,SWEENEY,ADAM,MONTGOMERY,MORRIS,TODD,CASSIDY,ROBB,STIRLING,WEBSTER,STUART,KANE,MCKENNA,FALCONER,GIBB,MCNEILL,TURNBULL,HOUSTON,HARVEY,DOWNIE,COCHRANE,LITTLE,MCARTHUR,POLLOCK,MACMILLAN,ORR,STEELE,HARRIS,LYNCH,DUNLOP,LOW,MCMAHON,NICHOLSON,ALLISON,MCCANN"
            .Split(',').Select(x => $"{x[0]}{x.Substring(1).ToLower()}").ToArray();

    public string GetRandomName() => userFirstNames[_rnd.Next(0, userFirstNames.Length - 1)];
    public string GetRandomLastName() => userLastNames[_rnd.Next(0, userLastNames.Length - 1)];

}