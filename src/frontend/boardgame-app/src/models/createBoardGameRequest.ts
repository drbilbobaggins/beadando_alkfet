export interface CreateBoardGameRequest {
  title: string;
  publisher: string;
  minPlayers: number;
  maxPlayers: number;
  estimatedPlayTime: number;
  category: string;
}
