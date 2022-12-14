export interface SteamWorkshopItem {
  publishedFileId: number;
  name: string;
  flags: number;
  fileType: number;
  fileSize: number;
  timeUpdated: number;
  previewUrl: URL;
  url: URL;

  enabled: boolean;
}
