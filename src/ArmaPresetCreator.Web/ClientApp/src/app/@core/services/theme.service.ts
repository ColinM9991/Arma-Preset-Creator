import {Injectable} from '@angular/core';

export enum Theme {
  Light = "light",
  Dark = "dark"
}

const THEME_KEY = 'APC-THEME';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  constructor() {
  }

  getOrDefault(): string {
    let storage_theme = localStorage.getItem(THEME_KEY);
    if (!storage_theme) {
      storage_theme = Theme.Light.toString();
      this.setTheme(storage_theme);
    }

    return storage_theme.toLowerCase();
  }

  setTheme(theme: string) {
    localStorage.setItem(THEME_KEY, theme.toLowerCase());
  }
}
