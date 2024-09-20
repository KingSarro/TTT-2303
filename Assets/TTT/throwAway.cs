/*
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
                int ran = Random.Range(1,3);//Change 3 to four after making the the get opposite cell
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
                else if(ran == 3){}
                else{}

            }
            //Fail case
            else{
                GetRandomCell();
            }
        }
*/


















/*
private void GetRandomAdjacentCell(int cellColum, int cellRow){
    Debug.Log("-----Entering GetRandomAdjacentCell");
    
    //first colum
    if(cellColum == 0){
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
    else if(cellColum == 1){
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
    else if(cellColum == 2){
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

*/