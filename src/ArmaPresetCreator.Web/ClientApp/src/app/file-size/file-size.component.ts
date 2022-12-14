import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-file-size',
  templateUrl: './file-size.component.html',
  styleUrls: ['./file-size.component.scss']
})
export class FileSizeComponent implements OnInit {
  @Input() sizeBytes!: number;

  constructor() {
  }

  ngOnInit(): void {
  }
  
  formatBytes(bytes: number, decimals = 2): string {
    if (bytes === 0) return '0 Bytes';

    const k = 1024;
    const dm = decimals < 0 ? 0 : decimals;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

    const i = Math.floor(Math.log(bytes) / Math.log(k));

    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
  }
}
