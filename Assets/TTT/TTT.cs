using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerOption
{
    NONE, //0
    X, // 1
    O // 2

}

public class TTT : MonoBehaviour{

    int turn = 0;
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
        if (GetWinner() != PlayerOption.NONE)
            return;

        
        //-----------GameOver----------------
        // if there's no winner, keep playing, otherwise end the game
        if(GetWinner() == PlayerOption.NONE){
            EndTurn();
        }
        else{
            Debug.Log("GAME OVER!");
        }
    }





//Gets A Random 
private void ChooseRandomCell(){
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
    cells[c,r].current = currentPlayer;
    board.UpdateCellVisual(c, r, currentPlayer);
    //adds to the turn and updates the last played variable
    turn++;
    lastPlay[0] = c; lastPlay[1] = r;
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
        ////turn++;
        ////lastPlay[0] = column; lastPlay[1] = row;

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
