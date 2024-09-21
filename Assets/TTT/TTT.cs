using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public enum PlayerOption
{
    NONE, //0
    X, // 1
    O // 2

}

public class TTT : MonoBehaviour{

    int turn = 1;
    int[] lastPlay = {-1,-1};// colum/row


    public int Rows;
    public int Columns;
    [SerializeField] BoardView board;

    PlayerOption currentPlayer = PlayerOption.X;
    Cell[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[Columns, Rows];

        board.InitializeBoard(Columns, Rows);

        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Columns; j++)
            {
                cells[j, i] = new Cell();
                cells[j, i].current = PlayerOption.NONE;
            }
        }
    }














//-------------------My Code--------------------------------



public void MakeOptimalMove(){
    // can't choose space if game is over
    if (GetWinner() != PlayerOption.NONE){
        return;
    }
    //------------Code

    //X will always be odd turns, O is always even turns.....
    //-------------------------

    //This is the starting turn
    //X's turn
    if(turn == 1){
        //Choose the middle position
        lastPlay[0] = 1; lastPlay[1] = 1;
        ChooseSpace(lastPlay[0],lastPlay[1]);
    }
    //O's turn
    else if(turn == 2){
        //If the X is in the center... choose a corner
        if(cells[1,1].current == PlayerOption.X){
            GetRandomCornerCell();//Last Play is updated in this method
        }
        //If x is in one of the corners...choose the center
        else if(cells[0,0].current == PlayerOption.X || cells[0,2].current == PlayerOption.X || cells[2,0].current == PlayerOption.X || cells[2,2].current == PlayerOption.X){
            if(cells[1,1].current == PlayerOption.NONE){
                //Sets the last play to the center cell
                lastPlay[0] = 1; lastPlay[1] = 1;
                ChooseSpace(lastPlay[0],lastPlay[1]);
            }
        }
        //If X is in one of the sides...choose the center...or the adjacent cell... or the opposite cell
        else if(cells[0,1].current == PlayerOption.X || cells[1,0].current == PlayerOption.X || cells[1,2].current == PlayerOption.X || cells[2,1].current == PlayerOption.X){
            //Random Center, Adj, or Opp
            int ran = Random.Range(1,4);//!Change 3 to four after making the the get opposite cell
            //Take the center cell
            if(ran == 1){
                if(cells[1,1].current == PlayerOption.NONE){
                    //Sets the last play to the center cell
                    lastPlay[0] = 1; lastPlay[1] = 1;
                    ChooseSpace(lastPlay[0],lastPlay[1]);
                }
                else{
                    GetRandomCell();
                }
            }
            //Take an adjacent cell
            else if(ran == 2){
                Debug.Log("Last Play: [" + lastPlay[0] + "," + lastPlay[1] + "]");
                GetRandomAdjacentCell(lastPlay[0], lastPlay[1]);
            }
            //Take an opposite cell
            else if(ran == 3){
                GetOppositeSideCell(lastPlay[0], lastPlay[1]);
            }
            else{
                GetRandomCell();
            }

        }
        //Fail case
        else{
            GetRandomCell();
        }
    }
}




//---Gets a random Cell---Calls the Choose Space Function---//
private void GetRandomCell(){
    Debug.Log("---Entering the Choose Random Cell function---");
    int c = -1,r = -1;
    //Gets a random cell
    do{ 
        int ran = Random.Range(1,10);
        Debug.Log("Random Num: " + ran);
        switch(ran){
            case 1:
                c = 0; r = 0;
                break;
            case 2:
                c = 0; r = 1;
                break;
            case 3:
                c = 0; r = 2;
                break;
            case 4:
                c = 1; r = 0;
                break;
            case 5:
                c = 1; r = 1;
                break;
            case 6:
                c = 1; r = 2;
                break;
            case 7:
                c = 2; r = 0;
                break;
            case 8:
                c = 2; r = 1;
                break;
            case 9:
                c = 2; r = 2;
                break;
        }
    }
    while(cells[c,r].current != PlayerOption.NONE); //Check for while loop

    //Sets the current cell and updates the visuals
    lastPlay[0] = c; lastPlay[1] = r;
    ChooseSpace(lastPlay[0], lastPlay[1]);
}
//---Gets a random Corner Cell---Calls the Choose Space Function---//
private void GetRandomCornerCell(){
    int c = -1,r = -1;
    //Gets a random cell
    Debug.Log("---Entering the Choose Random Corner Cell function---");
    do{ 
        int ran = Random.Range(1,5);
        //1:[0,0] 2:[0,2] 3:[2,0] 4:[2,2]
        Debug.Log("Random Num: " + ran);
        switch(ran){
            case 1:
                c = 0; r = 0;
                break;
            case 2:
                c = 0; r = 2;
                break;
            case 3:
                c = 2; r = 0;
                break;
            case 4:
                c = 2; r = 2;
                break;
        }
    }
    while(cells[c,r].current != PlayerOption.NONE); //Check for while loop

    //Sets the current cell and updates the visuals
    lastPlay[0] = c; lastPlay[1] = r;
    ChooseSpace(lastPlay[0], lastPlay[1]);
}
//---Gets a random Corner Cell---Calls the Choose Space Function---//
private void GetRandomSideCell(){
    int c = -1,r = -1;
    //Gets a random cell
    Debug.Log("---Entering the Choose Random Side Cell function---");
    do{ 
        int ran = Random.Range(1,5);
        //1:[0,1] 2:[1,0] 3:[1,2] 4:[2,1]
        Debug.Log("Random Num: " + ran);
        switch(ran){
            case 1:
                c = 0; r = 1;
                break;
            case 2:
                c = 1; r = 0;
                break;
            case 3:
                c = 1; r = 2;
                break;
            case 4:
                c = 2; r = 1;
                break;
        }
    }
    while(cells[c,r].current != PlayerOption.NONE); //Check for while loop

    //Sets the current cell and updates the visuals
    lastPlay[0] = c; lastPlay[1] = r;
    ChooseSpace(lastPlay[0], lastPlay[1]);
}
//---Gets a random Adjacent Cell---Calls the Choose Space Function---//
private void GetRandomAdjacentCell(int cellColumn, int cellRow){
    Debug.Log("-----Entering GetRandomAdjacentCell");
    //first colum
    if(cellColumn == 0){
        //first row
        if(cellRow == 0){
            //---In Cell [0,0]---//
            int ran = Random.Range(1,3);
            //cell[0,1]
            if(ran == 1){
                if(cells[0,1].current == PlayerOption.NONE){

                    lastPlay[0] = 0; lastPlay[1] = 1;
                }
                else{
                    //cell[1,0]
                    if(cells[1,0].current == PlayerOption.NONE){

                        lastPlay[0] = 1; lastPlay[1] = 0;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[1,0]
            else if(ran == 2){
                if(cells[1,0].current == PlayerOption.NONE){

                    lastPlay[0] = 1; lastPlay[1] = 0;
                }
                else{
                    //cell[0,1]
                    if(cells[0,1].current == PlayerOption.NONE){

                        lastPlay[0] = 0; lastPlay[1] = 1;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //second row
        else if(cellRow == 1){
            //---In Cell [0,1]---//
            int ran = Random.Range(1,3);
            //cell[0,0]
            if(ran == 1){  
                if(cells[0,0].current == PlayerOption.NONE){

                    lastPlay[0] = 0; lastPlay[1] = 0;
                }
                else{
                    //cell[0,2]
                    if(cells[0,2].current == PlayerOption.NONE){
                        lastPlay[0] = 0; lastPlay[1] = 2;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[0,2]
            else if(ran == 2){
                if(cells[0,2].current == PlayerOption.NONE){
                    lastPlay[0] = 0; lastPlay[1] = 2;
                }
                else{
                    //cell[0,0]
                    if(cells[0,0].current == PlayerOption.NONE){
                        lastPlay[0] = 0; lastPlay[1] = 0;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //third row
        else if(cellRow == 2){
            //---In Cell [0,2]---//
            int ran = Random.Range(1,3);
            //cell[0,1]
            if(ran == 1){  
                if(cells[0,1].current == PlayerOption.NONE){

                    lastPlay[0] = 0; lastPlay[1] = 1;
                }
                else{
                    //cell[1,2]
                    if(cells[1,2].current == PlayerOption.NONE){

                        lastPlay[0] = 1; lastPlay[1] = 2;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[1,2]
            else if(ran == 2){
                if(cells[1,2].current == PlayerOption.NONE){

                    lastPlay[0] = 1; lastPlay[1] = 2;
                }
                else{
                    //cell[0,1]
                    if(cells[0,1].current == PlayerOption.NONE){

                        lastPlay[0] = 0; lastPlay[1] = 1;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //Fail Safe
        else{
            Debug.Log("Problem in GetRandomAdjacentCell !!!");
            Debug.Log("Problem in Colum 1 !!!");
            Debug.Log("Row is out of range from 0-2 !!!");
            Debug.Log("Returning a random space !!!");

            GetRandomCell();
            return;
        }
    
    }
    //second colum
    else if(cellColumn == 1){
        //first row
        if(cellRow == 0){
            //---In Cell [1,0]---//
            int ran = Random.Range(1,3);

            //cell[0,0]
            if(ran == 1){
                if(cells[0,0].current == PlayerOption.NONE){

                    lastPlay[0] = 0; lastPlay[1] = 0;
                }
                else{
                    //cell[2,0]
                    if(cells[2,0].current == PlayerOption.NONE){

                        lastPlay[0] = 2; lastPlay[1] = 0;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[2,0]
            else if(ran == 2){
                if(cells[2,0].current == PlayerOption.NONE){

                    lastPlay[0] = 2; lastPlay[1] = 0;
                }
                else{
                    //cell[0,0]
                    if(cells[0,0].current == PlayerOption.NONE){

                        lastPlay[0] = 0; lastPlay[0] = 1;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //second row
        else if(cellRow == 1){
            //---In Cell [1,1]---//
            ////We will do nothing for the middle cell, assuming it is taken
            //!We will get a random corner instead, because the other player has taken the middle
            GetRandomCornerCell();
            return;
        }
        //third row
        else if(cellRow == 2){
            //---In Cell [1,2]---//
            int ran = Random.Range(1,3);
            //cell[0,2]
            if(ran == 1){  
                if(cells[0,2].current == PlayerOption.NONE){

                    lastPlay[0] = 0; lastPlay[1] = 2;
                }
                else{
                    //cell[2,2]
                    if(cells[2,2].current == PlayerOption.NONE){

                        lastPlay[0] = 2; lastPlay[1] = 2;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[2,2]
            else if(ran == 2){
                if(cells[2,2].current == PlayerOption.NONE){

                    lastPlay[0] = 2; lastPlay[1] = 2;
                }
                else{
                    //cell[0,2]
                    if(cells[0,2].current == PlayerOption.NONE){

                        lastPlay[0] = 0; lastPlay[1] = 2;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //Fail Safe
        else{
            Debug.Log("Problem in GetRandomAdjacentCell !!!");
            Debug.Log("Problem in Colum 1 !!!");
            Debug.Log("Row is out of range from 0-2 !!!");
            Debug.Log("Returning a random space !!!");

            GetRandomCell();
            return;
        }
    
    }
    //third colum
    else if(cellColumn == 2){
        if(cellRow == 0){
            //---In Cell [2,0]---//
            int ran = Random.Range(1,3);
            //cell[2,1]
            if(ran == 1){
                if(cells[2,1].current == PlayerOption.NONE){

                    lastPlay[2] = 0; lastPlay[1] = 1;
                }
                else{
                    //cell[1,0]
                    if(cells[1,0].current == PlayerOption.NONE){

                        lastPlay[0] = 1; lastPlay[1] = 0;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[1,0]
            else if(ran == 2){
                if(cells[1,0].current == PlayerOption.NONE){

                    lastPlay[0] = 1; lastPlay[1] = 0;
                }
                else{
                    //cell[2,1]
                    if(cells[2,1].current == PlayerOption.NONE){

                        lastPlay[0] = 2; lastPlay[1] = 1;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //second row
        else if(cellRow == 1){
            //---In Cell [2,1]---//
            int ran = Random.Range(1,3);
            //cell[2,0]
            if(ran == 1){  
                if(cells[2,0].current == PlayerOption.NONE){

                    lastPlay[0] = 2; lastPlay[1] = 0;
                }
                else{
                    //cell[2,2]
                    if(cells[2,2].current == PlayerOption.NONE){
                        lastPlay[0] = 2; lastPlay[1] = 2;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[2,2]
            else if(ran == 2){
                if(cells[2,2].current == PlayerOption.NONE){
                    lastPlay[0] = 2; lastPlay[1] = 2;
                }
                else{
                    //cell[2,0]
                    if(cells[2,0].current == PlayerOption.NONE){
                        lastPlay[0] = 2; lastPlay[1] = 0;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //third row
        else if(cellRow == 2){
            //---In Cell [2,2]---//
            int ran = Random.Range(1,3);
            //cell[2,1]
            if(ran == 1){  
                if(cells[2,1].current == PlayerOption.NONE){

                    lastPlay[0] = 2; lastPlay[1] = 1;
                }
                else{
                    //cell[1,2]
                    if(cells[1,2].current == PlayerOption.NONE){

                        lastPlay[0] = 1; lastPlay[1] = 2;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //cell[1,2]
            else if(ran == 2){
                if(cells[1,2].current == PlayerOption.NONE){

                    lastPlay[0] = 1; lastPlay[1] = 2;
                }
                else{
                    //cell[2,1]
                    if(cells[2,1].current == PlayerOption.NONE){

                        lastPlay[0] = 2; lastPlay[1] = 1;
                    }
                    //Fail safe
                    else{
                        GetRandomCell();
                        return;
                    }
                }
            }
            //Fail safe
            else{
                GetRandomCell();
                return;
            }
        
        }
        //Fail Safe
        else{
            Debug.Log("Problem in GetRandomAdjacentCell !!!");
            Debug.Log("Problem in Colum 1 !!!");
            Debug.Log("Row is out of range from 0-2 !!!");
            Debug.Log("Returning a random space !!!");

            GetRandomCell();
            return;
        }
    
    }
    //Fail Safe
    else{
        Debug.Log("Problem in GetRandomAdjacentCell !!!");
        Debug.Log("Colum is out of range from 0-2 !!!");
        Debug.Log("Returning a random space !!!");

        GetRandomCell();
        return;
    }

    ChooseSpace(lastPlay[0],lastPlay[1]);
}
//---Gets a random Opposite Side Cell---Calls the Choose Space Function---//
private void GetOppositeSideCell(int cellColumn, int cellRow){
    Debug.Log("---Entering the Choose Opposite Side Cell function---");
    int c = -1,r = -1;

    //1:[0,1]-->[2,1]  2:[1,0]-->[1,2]  3:[1,2]-->[1,0]  4:[2,1]-->[0,1]
    if(cellColumn == 0){
        if(cellRow == 1){
            c = 2; r = 1;
        }
        else{
            Debug.Log("Problem in Get Random Opposite Side Cell!!! Row is not 1 !!!");
            Debug.Log("Returning a random space!!!");
            GetRandomCell();
            return;
        }
    }
    else if(cellColumn == 1){
        if(cellRow == 0){
            c = 1; r = 2;
        }
        else if(cellRow == 2){
            c = 1; r = 0;
        }
        else{
            Debug.Log("Problem in Get Random Opposite Side Cell!!! Row is not 0 or 2 !!!");
            Debug.Log("Returning a random space!!!");
            GetRandomCell();
            return;
        }
    }
    else if (cellColumn == 2){
        if(cellRow == 1){
            c = 0; r = 1;
        }
        else{
            Debug.Log("Problem in Get Random Opposite Side Cell!!! Row is not 1 !!!");
            Debug.Log("Returning a random space!!!");
            GetRandomCell();
            return;
        }
    }
    else{
        Debug.Log("Problem in Get Random Opposite Side Cell!!! Column is out of range from 0-2!!!");
        Debug.Log("Returning a random space!!!");
        GetRandomCell();
        return;
    }
    
    //Sets the current cell and updates the visuals
    lastPlay[0] = c; lastPlay[1] = r;
    ChooseSpace(lastPlay[0], lastPlay[1]);
}
//---Gets a random Opposite Corner Cell---Calls the Choose Space Function---//
private void GetRandomOppositeCornerCell(int cellColumn, int cellRow, bool getDiagonal){
    Debug.Log("---Entering the Choose Opposite Corner Cell function---");
    int c = -1,r = -1;

    //1:[0,0] 2:[0,2] 3:[2,0] 4:[2,2]

    

    //Random horizontal or vertical side side
    if(getDiagonal == false){
        int ran = Random.Range(1,3);
        // 1:[0,2][2,0]  2:[0,0][2,2]  3:[0,0][2,2]  4:[0,2][2,0]
        if(cellColumn == 0){
            if(cellRow == 0){
                if(ran == 1){
                    c = 0; r = 2;
                }
                else if(ran == 2){
                    c = 2; r = 0;
                }
                //Fail safe
                else{
                    Debug.Log("Problem in Get Random Opposite Corner Cell!!! Random number is not 1 or 2!!!");
                    Debug.Log("Returning a random space!!!");
                    GetRandomCell();
                    return;
                }
            }
            else if(cellRow == 2){
                if(ran == 1){
                    c = 0; r = 0;
                }
                else if(ran == 2){
                    c = 2; r = 2;
                }
                //Fail safe
                else{
                    Debug.Log("Problem in Get Random Opposite Corner Cell!!! Random number is not 1 or 2!!!");
                    Debug.Log("Returning a random space!!!");
                    GetRandomCell();
                    return;
                }
            }
            //fail safe
            else{
                Debug.Log("Problem in Get Random Opposite Corner Cell!!! Row is not 0 or 2!!!");
                Debug.Log("Returning a random space!!!");
                GetRandomCell();
                return;
            }
        
        }
        else if (cellColumn == 2){
            if(cellRow == 0){
                if(ran == 1){
                    c = 0; r = 0;
                }
                else if(ran == 2){
                    c = 2; r = 2;
                }
                //Fail safe
                else{
                    Debug.Log("Problem in Get Random Opposite Corner Cell!!! Random number is not 1 or 2!!!");
                    Debug.Log("Returning a random space!!!");
                    GetRandomCell();
                    return;
                }
            }
            else if(cellRow == 2){
                if(ran == 1){
                    c = 0; r = 2;
                }
                else if(ran == 2){
                    c = 2; r = 0;
                }
                //Fail safe
                else{
                    Debug.Log("Problem in Get Random Opposite Corner Cell!!! Random number is not 1 or 2!!!");
                    Debug.Log("Returning a random space!!!");
                    GetRandomCell();
                    return;
                }
            }
            //fail safe
            else{
                Debug.Log("Problem in Get Random Opposite Corner Cell!!! Row is not 0 or 2!!!");
                Debug.Log("Returning a random space!!!");
                GetRandomCell();
                return;
            }
        
        }
        
        //fail safe
        else{
            Debug.Log("Problem in Get Random Opposite Corner Cell!!! Column is not 0 or 2!!!");
            Debug.Log("Is NOT trying to get Diagonal!!!");
            Debug.Log("Returning a random space!!!");
            GetRandomCell();
            return;
        }
    
    }
    //Gets the opposite diagonal
    else{
        // 1:[2,2]  2:[2,0]  3:[0,2]  4:[0,0]
        if(cellColumn == 0){
            //[0,0] --> [2,2]
            if(cellRow == 0){
                c = 2; r = 2;
            }
            //[0,0] --> [2,2]
            else if(cellRow == 2){
                c = 0; r = 0;
            }
            
            //fail safe
            else{
                Debug.Log("Problem in Get Random Opposite Corner Cell!!! Row is not 0 or 2!!!");
                Debug.Log("Returning a random space!!!");
                GetRandomCell();
                return;
            }
        
        }
        else if(cellColumn == 2){
            //[2,0] --> [0,2]
            if(cellRow == 0){
                c = 0; r = 2;
            }
            //[2,2] --> [0,0]
            else if(cellRow == 2){
                c = 0; r = 0;
            }
            
            //fail safe
            else{
                Debug.Log("Problem in Get Random Opposite Corner Cell!!! Row is not 0 or 2!!!");
                Debug.Log("Returning a random space!!!");
                GetRandomCell();
                return;
            }
        
        }
        //fail safe
        else{
            Debug.Log("Problem in Get Random Opposite Corner Cell!!! Column is not 0 or 2!!!");
            Debug.Log("Is trying to get Diagonal!!!");
            Debug.Log("Returning a random space!!!");
            GetRandomCell();
            return;
        }
    }
    
    //Sets the current cell and updates the visuals
    lastPlay[0] = c; lastPlay[1] = r;
    ChooseSpace(lastPlay[0], lastPlay[1]);
}


//Checks if a winning move is available
//TODO: probably will need to make this return a value (most likely a bool)
private void CheckForWinningMoves(){
    //Winning moves for X
        //Take Win if its Xs turn
        //Blocking if its Ys turn
    //Winning moves for Y
        //Take Win if its Ys turn
        //Blocking if its Xs turn
}












//-------------------Teacher Code--------------------------------

    public void ChooseSpace(int column, int row)
    {
        // can't choose space if game is over
        if (GetWinner() != PlayerOption.NONE)
            return;

        // can't choose a space that's already taken
        if (cells[column, row].current != PlayerOption.NONE)
            return;

        // set the cell to the player's mark
        cells[column, row].current = currentPlayer;

        //!Sharri'a - Add the chosen cells to the lastPlay
        lastPlay[0] = column;
        lastPlay[1] = row;

        // update the visual to display X or O
        board.UpdateCellVisual(column, row, currentPlayer);

        // if there's no winner, keep playing, otherwise end the game
        if(GetWinner() == PlayerOption.NONE){
            EndTurn();
        }
        else
        {
            Debug.Log("GAME OVER!");
        }
    }

    public void EndTurn(){
        //turn++; //TODO:
        // increment player, if it goes over player 2, loop back to player 1
        currentPlayer += 1;
        if ((int)currentPlayer > 2)
            currentPlayer = PlayerOption.X;

            turn++;
            Debug.Log("Turn: " + turn);
    }

    public PlayerOption GetWinner()
    {
        // sum each row/column based on what's in each cell X = 1, O = -1, blank = 0
        // we have a winner if the sum = 3 (X) or -3 (O)
        int sum = 0;

        // check rows
        for (int i = 0; i < Rows; i++)
        {
            sum = 0;
            for (int j = 0; j < Columns; j++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check columns
        for (int j = 0; j < Columns; j++)
        {
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check diagonals
        // top left to bottom right
        sum = 0;
        for(int i = 0; i < Rows; i++)
        {
            int value = 0;
            if (cells[i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        // top right to bottom left
        sum = 0;
        for (int i = 0; i < Rows; i++)
        {
            int value = 0;

            if (cells[Columns - 1 - i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[Columns - 1 - i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        return PlayerOption.NONE;
    }
}
