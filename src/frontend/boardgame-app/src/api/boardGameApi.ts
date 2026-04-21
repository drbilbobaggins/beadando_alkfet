import type { BoardGame } from '../models/boardGame';
import type { CreateBoardGameRequest } from '../models/createBoardGameRequest';
import { API_BASE_URL } from '../utils/config';

export async function getBoardGames(): Promise<BoardGame[]> {
  const response = await fetch(`${API_BASE_URL}/boardgames`);

  if (!response.ok) {
    throw new Error(`Failed to load board games. Status: ${response.status}`);
  }

  return (await response.json()) as BoardGame[];
}

export async function createBoardGame(
  boardGame: CreateBoardGameRequest,
): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/boardgames`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(boardGame),
  });

  if (!response.ok) {
    throw new Error(`Failed to create board game. Status: ${response.status}`);
  }
}

export async function deleteBoardGame(id: string): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/boardgames/${id}`, {
    method: 'DELETE',
  });

  if (!response.ok) {
    throw new Error(`Failed to delete board game. Status: ${response.status}`);
  }
}
