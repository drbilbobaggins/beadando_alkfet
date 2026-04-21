export interface BoardGame {
  id: string;
  title: string;
  publisher: string;
  minPlayers: number;
  maxPlayers: number;
  estimatedPlayTime: number;
  category: string;
}
