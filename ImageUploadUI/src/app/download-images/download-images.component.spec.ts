import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DownloadImagesComponent } from './download-images.component';

describe('DownloadImagesComponent', () => {
  let component: DownloadImagesComponent;
  let fixture: ComponentFixture<DownloadImagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DownloadImagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DownloadImagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
