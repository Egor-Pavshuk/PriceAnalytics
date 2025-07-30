<script setup>
import TopBar from './components/TopBar.vue'
import FilterBar from './components/FilterBar.vue'
import DailyPrices from './components/DailyPrices.vue'
import UploadFiles from './components/UploadFiles.vue'
import RotateLoader from './components/RotateLoader.vue'
import { ref } from 'vue'

const dailyPrices = ref([]);
const orders = ref([]);
const isLoadingActive = ref(false)

function updatePrices(newPrices)
{
  dailyPrices.value = newPrices;
}

function updateOrders(newOrders)
{
  orders.value = newOrders;
}

</script>

<template>
  <div class="back">
    <img class="logo" src="./assets/TopPolygon.svg" />
  </div>
    <TopBar />

    <Transition name="fade">
      <div v-if="isLoadingActive" class="loader-wrapper">
        <RotateLoader :loading = true class="loader"/>
      </div>
    </Transition>

    <FilterBar @update-prices="updatePrices" @update-orders="updateOrders" @update-loading= "isLoadingActive = $event"/>
    <DailyPrices :pricesData="dailyPrices" :ordersData="orders"/>
    <UploadFiles />
</template>

<style scoped>
.back
{
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 40%;
}

.logo
{
  position: absolute;
  width: 100%;
  height: 100%;
}

.loader-wrapper{
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: rgba(255, 255, 255, 0.4);
  z-index: 9999;
}

.loader{
  position: absolute;
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
