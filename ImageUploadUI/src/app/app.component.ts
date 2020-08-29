import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from './Services/api.service';
import { Guid } from "guid-typescript";

declare var jQuery: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ImageUploadUI';

  uploadedFile: File = null;
  myReader: FileReader = new FileReader();

  imageBase64: string = '';
  imageName: string = '';

  //@ViewChild('fileAttachment') inputFileAttachment;
  constructor(private apiService: ApiService) { }


  ngOnInit() {
    this.apiService.clearImg.subscribe(() => {
      this.uploadedFile = null;
      this.imageBase64 = '';
      this.imageName = '';
    });
  }

  uploadFile() {
    let element: HTMLElement = document.getElementById('fileAttachment') as HTMLElement;
    element.click();
  }

  fileChange(event) {
    let _newGuid: string = Guid.create().toString();
    this.uploadedFile = event.target.files[0];
    this.myReader.readAsDataURL(this.uploadedFile);
    this.myReader.onloadend = (e: any) => {

      let index: number = this.uploadedFile.name.lastIndexOf('.');

      let fileExtension = this.uploadedFile.name.substring(index + 1);

      this.imageName = _newGuid + '.' + fileExtension;
      this.imageBase64 = <string>this.myReader.result;
      this.apiService.UploadImage(this.uploadedFile, this.imageName);

    }
  }



}
