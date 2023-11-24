import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import "@/assets/CSS/all.css";
import Toast from "vue-toastification";
// Import the CSS or use your own!
const options = {
  // You can set your default options here
};

import "vue-toastification/dist/index.css";
import "./icon";
// default everything to Vue 3 behavior, and only enable compat
// for certain features
createApp(App)
  .use(store)
  .use(Toast, options)
  .component("font-awesome-icon", FontAwesomeIcon)
  .use(router)
  .mount("#app");
