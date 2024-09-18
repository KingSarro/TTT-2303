/*

//If the center option is free
        if (cells[1,1].current == PlayerOption.NONE){
            cells[1,1].current = currentPlayer;
            turn++;
            board.UpdateCellVisual(1, 1, currentPlayer);
            lastPlay[0] = 1; lastPlay[1] = 1;

            //-----------GameOver----------------
            // if there's no winner, keep playing, otherwise end the game
            if(GetWinner() == PlayerOption.NONE){
                EndTurn();
            }
            else{
                Debug.Log("GAME OVER!");
            }
        }
        
        else{
            //If all of the corner pieces are free
            if(cells[0,0].current == PlayerOption.NONE && cells[0,2].current == PlayerOption.NONE && cells[2,0].current == PlayerOption.NONE && cells[2,2].current == PlayerOption.NONE){
            ////if the current player has the middle
                if (cells[1,1].current == currentPlayer){
                    //pick a corner
                    int ran = Random.Range(1,5);
                    switch(ran){
                        case 1:
                            cells[0,0].current = currentPlayer;
                            board.UpdateCellVisual(0, 0, currentPlayer);
                            lastPlay[0] = 0; lastPlay[1] = 0;
                            break;
                        case 2:
                            cells[0,2].current = currentPlayer;
                            board.UpdateCellVisual(0, 2, currentPlayer);
                            lastPlay[0] = 0; lastPlay[1] = 2;
                            break;
                        case 3:
                            cells[2,0].current = currentPlayer;
                            board.UpdateCellVisual(2, 0, currentPlayer);
                            lastPlay[0] = 2; lastPlay[1] = 0;
                            break;
                        case 4:
                            cells[2,2].current = currentPlayer;
                            board.UpdateCellVisual(2, 2, currentPlayer);
                            lastPlay[0] = 2; lastPlay[1] = 2;
                            break;
                    }

*/