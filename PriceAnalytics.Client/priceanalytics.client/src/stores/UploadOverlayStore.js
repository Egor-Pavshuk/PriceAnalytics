import { ref } from 'vue';

export const IsOverlayVisible = ref(false);

export function ShowOverlay()
{
    IsOverlayVisible.value = true;
}

export function HideOverlay()
{
    IsOverlayVisible.value = false;
}