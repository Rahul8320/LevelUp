import { AbstractControl, ValidatorFn } from '@angular/forms';

export function imageUrlsValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const imagesArray = control.value as string[];

    if (!imagesArray || imagesArray.length === 0) {
      return { required: true }; // At least one image URL is required
    }

    for (const imageUrl of imagesArray) {
      if (!/^(http|https):\/\/[^ "]+$/.test(imageUrl)) {
        return { invalidImageUrl: true }; // Image URL pattern validation failed
      }
    }

    return null; // Validation passed
  };
}
