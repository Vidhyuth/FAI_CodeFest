import { Component, OnInit } from '@angular/core';
import { ApiService } from '../Services/api.service';

@Component({
  selector: 'app-download-images',
  templateUrl: './download-images.component.html',
  styleUrls: ['./download-images.component.css']
})
export class DownloadImagesComponent implements OnInit {
  //files: string[] = ['abcd', 'dasd'];
  constructor(public apiService: ApiService) {

  }

  ngOnInit(): void {

  }

  DownloadImage(fileName: string) {
    console.log(fileName);
    this.apiService.downloadImage(fileName).subscribe((res: any) => {
      var anchor = document.createElement('a');

      let index: number = fileName.lastIndexOf('.');

      let fileExtension = fileName.substring(index + 1);

      anchor.href = 'data:image/' + fileExtension + ';base64,' + res.Result;
      anchor.target = '_blank';
      anchor.download = fileName;
      anchor.click();
    }, () => { });
  }
}
