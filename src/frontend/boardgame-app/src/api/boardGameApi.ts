import type { BoardGame } from '../models/boardGame';
import { API_BASE_URL } from '../utils/config';

export async function getBoardGames(): Promise<BoardGame[]> {
  const response = await fetch(`${API_BASE_URL}/boardgames`);

  if (!response.ok) {
    throw new Error(`Failed to load board games. Status: ${response.status}`);
  }

  return (await response.json()) as BoardGame[];
}
