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

  onSubmit(event): void {
    
  }

  onFileChanged(event): void {
    const file: File = event.target.files[0];
    if(file) {
      const formData = new FormData();
      formData.append("file", file);
      this.logUploadService.uploadFileToApi(formData).subscribe();
    }
  }

  ngOnInit(): void {
  }

}
