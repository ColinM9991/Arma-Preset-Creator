import {AfterViewInit, Component, Renderer2} from '@angular/core';
import {Theme, ThemeService} from "./@core/services/theme.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterViewInit {
  date = new Date();
  themes = Object.keys(Theme);
  selected_theme = '';

  constructor(
    private renderer: Renderer2,
    private themeService: ThemeService) {
    this.selected_theme = this.themeService.getOrDefault();
  }

  onThemeChange() {
    this.changeTheme(this.selected_theme, true);
  }

  private changeTheme(theme: string, change: boolean) {
    this.renderer.setAttribute(document.body, "data-bs-theme", theme.toLowerCase());
    if (change) {
      this.themeService.setTheme(theme);
    }
  }

  ngAfterViewInit(): void {
    this.changeTheme(this.selected_theme, false);
  }
}
