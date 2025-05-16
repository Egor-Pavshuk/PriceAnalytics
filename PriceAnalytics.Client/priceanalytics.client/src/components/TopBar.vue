<script setup>
import { ShowOverlay } from '../stores/UploadOverlayStore';

</script>

<template>
    <div class="top-bar">
        <button class="top-btn">Show order prices</button>
        <button class="top-btn">Show daily prices</button>
        <button class="top-btn" @click="ShowOverlay">
          Upload files
        </button>
        <button class="user-btn">
          <img class="user-icon" src="../assets/UserCircle.svg" />
        </button>
    </div>
</template>

<style scoped>
.top-bar 
{
  display: flex;
  align-items: center;
  justify-content: flex-start; 
  gap: 16px; 
  padding: 12px;
  background-color: rgba(255, 255, 255, 0.3);
  border-bottom: 1px solid #a2c7ea;
  width: 100%;
  box-sizing: border-box;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 1000;

  backdrop-filter: blur(8px);
  -webkit-backdrop-filter: blur(8px);
}

.top-btn 
{
  padding: 8px 16px;
  border: none;
  background-color: rgba(255, 255, 255, 0.6);
  border: 1px #a2c7ea solid;
  border-radius: 6px;
  cursor: pointer;
  transition: background 0.2s ease;
}

.top-btn:hover 
{
  background-color: #ddd;
}

.user-icon
{
  width: 40px;
  height: 40px;
}

.user-btn
{
  margin-left: auto;
  width: 40px;
  height: 40px;
  border: none;
  background-color: rgba(255, 255, 255, 0.6);
  border-radius: 50%;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background 0.2s ease;
}

.user-btn:hover 
{
  background-color: #ddd;
}
</style>