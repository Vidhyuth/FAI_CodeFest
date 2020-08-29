import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  webApiUrl: string = 'http://localhost:58171/';
  clearImg: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor(private http: HttpClient, private toastrService: ToastrService) { }

  listOfImagesUploaded: string[] = [];

  UploadImage(file: File, imageGuidName: string) {
    const formData = new FormData();
    //bulkUpload[i].file.name + '?' + bulkUpload[i].guid
    formData.append('uploadFile', file, imageGuidName);
    this.postCall('Values/SaveEncyptImg', formData).subscribe(() => {
      this.listOfImagesUploaded.push(imageGuidName);
      this.toastrService.success("Image Uploaded successfully");
      this.ClearCurrentImage();

    }, () => { });
  }
  ClearCurrentImage() {
    this.clearImg.emit(true);
  }

  postCall(url: string, body: any) {
    let postUrl = this.webApiUrl + url;
    let headers1 = new HttpHeaders();
    headers1.append('Content-Type', 'multipart/form-data');
    headers1.append('Accept', 'application/json');
    return this.http.post(postUrl, body, { headers: headers1 });
  }

  getCall(url: string) {
    let getUrl = this.webApiUrl + url;
    // let headers1 = new HttpHeaders();
    // headers1.append('Content-Type', 'multipart/form-data');
    // headers1.append('Accept', 'application/json');
    return this.http.get(getUrl);
  }

  downloadImage(imageName: string) {
    return this.getCall('Values/DownloadImage?ImageName=' + imageName);
  }

}
