<script setup>
import { ref } from 'vue';
import { HideOverlay } from '../stores/UploadOverlayStore';
import { IsOverlayVisible } from '../stores/UploadOverlayStore';
import RotateLoader from './RotateLoader.vue';
import Checkmark from '../assets/Checkmark.svg';
import Cross from '../assets/Cross.svg';

const PRICES = "prices";
const UPLOADURL = "https://localhost:7260/api/files/upload/";
const selectedInput = ref(PRICES);
const isLoading = ref(false);
const showCheckmark = ref(false);
const showCross = ref(false);
let uploadFile = null;

function ChangeInput(inputName)
{
  uploadFile = null;
  selectedInput.value = inputName;
}

function HandleChange(e)
{
  uploadFile = e.target.files[0];
}

async function UploadFile()
{
  if(uploadFile)
  {
    isLoading.value = true;
    const formData = new FormData();
    let uploadURL = UPLOADURL;

    if(selectedInput.value === PRICES)
    {
      uploadURL += "prices";
    }
    else
    {
      uploadURL += "order";
    }

    formData.append("file", uploadFile);
    const response = await fetch(uploadURL,
    {
      method : 'POST',
      body: formData,
    }).catch(() =>
    {
      isLoading.value = false;
      showCross.value = true;
      setTimeout(() => {
        showCross.value = false;
      }, 2000)
    });

    isLoading.value = false;
    if(response.ok)
    {
      showCheckmark.value = true;
      setTimeout(() => {
        showCheckmark.value = false;
      }, 2000);
    }
    else
    {
      alert("Error: " + await response.text());
    }
  }
}

</script>

<template>
  <div class="overlay" v-if="IsOverlayVisible">
    <div class="container">
      <button class="close-btn" @click="HideOverlay">&times;</button>
      <div class="text-block">
        <h3>Upload files</h3>
      </div>
      <div class="buttons">
        <button class="btn prices-drop" :class="{ active: selectedInput === 'prices' }" @click="ChangeInput('prices')">Prices</button>
        <button class="btn order-drop" :class="{ active: selectedInput === 'order' }" @click="ChangeInput('order')">Order</button>
      </div>
      <hr/>
      <br/>
      <div class="drop-block">
        <input type="file" class="drop-input" v-if="selectedInput === 'prices'" @change="HandleChange"/>
        <input type="file" class="drop-input" v-if="selectedInput === 'order'" @change="HandleChange"/>
      </div>
      <Transition name="fade">
        <button class="btn upload-btn" @click="UploadFile" v-if="!isLoading && !showCheckmark && !showCross">Upload</button>
      </Transition>
      <Transition name="fade">
        <RotateLoader :loading = isLoading class="loader"/>
      </Transition>
      <Transition name="fade">
        <img :src="Checkmark" alt="Success" class="check" v-if="showCheckmark" />
      </Transition>
      <Transition name="fade">
        <img :src="Cross" alt="Faild" class="check" v-if="showCross" />
      </Transition>
    </div>
  </div>
</template>

<style scoped>
h3 {
  font-size: 28px;
  font-weight: 600;
  margin: 10px 0 10px 10px;
}

.overlay{
  align-items: center;
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.5);
  z-index: 9999;
}

.container{
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 450px;
  height: 200px;
  background: rgb(238, 234, 234);
  border-radius: 10px;
  border: 2px solid #8b8989;
  display: flex;
  flex-direction: column;
}

.drop-block{
  width: 100%;
  height: 100%;
  margin: 0 0 0 10px;
}

.btn{
  padding: 8px 16px;
  border: none;
  background-color: rgba(255, 255, 255, 0.6);
  border: 1px #a2c7ea solid;
  border-radius: 3px 3px 0 0;
  cursor: pointer;
  transition: background 0.2s ease;
}

.btn:hover{
  background-color: #ddd;
}

.btn.active{
  background-color: #a2c7ea;
  color: white;
}

.buttons{
  margin: 0 0 0 3px;
  justify-content: flex-start;
  gap: 16px;
}

.close-btn{
  position: absolute;
  font-size: 20px;
  width: 25px;
  height: 25px;
  top: 5%;
  right: 3%;
  background: transparent;
  cursor: pointer;
  border: 0;
}

.upload-btn{
  position: absolute;
  padding: 6px 10px;
  bottom: 10px;
  right: 15px;
  border-radius: 3px 3px 3px 3px;
}

.loader{
  position: absolute;
  bottom: 20px;
  right: 40px;
}

.check{
  position: absolute;
  width: 40px;
  height: 40px;
  bottom: 15px;
  right: 30px;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

</style>
