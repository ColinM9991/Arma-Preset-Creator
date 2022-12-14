export interface ArmaPresetRequest {
  name: string;
  items: ArmaAddon[];
}

export interface ArmaAddon {
  name: string;
  url: URL;
}
