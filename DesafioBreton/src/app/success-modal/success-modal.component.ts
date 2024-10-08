import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-success-modal',
  standalone: true,
  templateUrl: './success-modal.component.html',
  styleUrls: ['./success-modal.component.css'],
})
export class SuccessModalComponent implements OnInit {
  progress = 100;
  intervalId: any;

  constructor(
    public dialogRef: MatDialogRef<SuccessModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { entityName: string }
  ) {}

  ngOnInit(): void {
    this.intervalId = setInterval(() => {
      this.progress -= 1;
      if (this.progress <= 0) {
        this.dialogRef.close();
      }
    }, 30);
  }

  onOkClick(): void {
    clearInterval(this.intervalId);
    this.dialogRef.close();
  }
}
