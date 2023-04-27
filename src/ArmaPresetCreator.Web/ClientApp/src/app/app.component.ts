import {AfterViewInit, Component, Renderer2} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterViewInit {
  date = new Date();

  constructor(private renderer: Renderer2) {
  }

  onThemeChange(event: any) {
    let newTheme = event.target.value;
    this.changeTheme(newTheme);
  }

  private changeTheme(themeName: string) {
    this.renderer.setAttribute(document.body, "data-bs-theme", themeName);
  }

  ngAfterViewInit(): void {
    this.changeTheme("dark");
  }
}
