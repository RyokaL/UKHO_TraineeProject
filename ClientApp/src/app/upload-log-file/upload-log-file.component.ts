import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LogFileUploadService } from 'src/services/log-file-upload-service/log-file-upload.service';

@Component({
  selector: 'app-upload-log-file',
  templateUrl: './upload-log-file.component.html',
  styleUrls: ['./upload-log-file.component.css']
})
export class UploadLogFileComponent implements OnInit {

  constructor(private logUploadService: LogFileUploadService) { }

  formGroup = new FormGroup({
      LogFile: new FormControl([Validators.required])
  });

  file: File = null;
  fileUploaded = false;

  onSubmit(event): void {
    if(this.file) {
      const formData = new FormData();
      formData.append("file", this.file);
      this.logUploadService.uploadFileToApi(formData).subscribe();
      this.file = null;
      this.fileUploaded = true;
      this.formGroup.reset();
    }
  }

  onFileChanged(event): void {
    this.fileUploaded = false;
    this.file = event.target.files[0];
  }

  ngOnInit(): void {
    this.file = null;
    this.fileUploaded = false;
  }

}
