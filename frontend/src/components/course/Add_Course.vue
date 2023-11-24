<template>
  <div class="add_exam">
    <h1>
      {{ this.$route.query.action == "edit" ? "Cập nhật" : "Thêm" }} khóa học
    </h1>
    <form @submit.prevent="onsubmit" class="from_add_exam">
      <div class="group_add_exam">
        <div class="name_add_exam">
          <label>Tên khóa học</label>
          <input v-model="data.name" type="text" />
          <span class="err" v-if="err.name">{{ err.name }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <div class="language_add_exam">
          <label> Ngôn Ngữ </label>
          <div class="dropdown">
            <div
              @click="status_dropdown_languages = !status_dropdown_languages"
              class="dropdown_input"
            >
              <span class="text">
                {{
                  data.id_lang == null
                    ? "Chọn ngôn ngữ"
                    : $store.getters["languages/getbyid"](data.id_lang)[0].name
                }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="
                status_dropdown_languages == true ? 'dropdown_menu_show' : null
              "
              class="dropdown_menu"
            >
              <div
                @click="
                  (data.id_lang = item.id), (status_dropdown_languages = false)
                "
                class="dropdown_item"
                v-for="item in languages"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
          <span class="err" v-if="err.languages">{{ err.languages }}</span>
        </div>
        <!-- dropdown lanquage -->
      </div>

      <!-- //group price and ducation -->
      <div class="group_add_exam">
        <div class="price_add_exam">
          <label>Lệ phí</label>
          <input v-model="data.price" type="number" />
          <span class="err" v-if="err.price">{{ err.price }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <div class="ducation_add_exam">
          <label> Chứng chỉ </label>
          <div class="dropdown">
            <div
              @click="status_dropdown_certicate = !status_dropdown_certicate"
              class="dropdown_input"
            >
              <span class="text">
                {{
                  data.id_cer == null
                    ? "Chọn chứng chỉ"
                    : $store.getters["certificate/getbyid"](data.id_cer)[0].name
                }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="
                status_dropdown_certicate == true ? 'dropdown_menu_show' : null
              "
              class="dropdown_menu"
            >
              <div
                @click="
                  (data.id_cer = item.id), (status_dropdown_certicate = false)
                "
                class="dropdown_item"
                v-for="item in certificate"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
          <span class="err" v-if="err.certificate">{{ err.certificate }}</span>
        </div>
        <!-- dropdown lanquage -->
      </div>
      <!-- end grop price and ducation -->

      <!-- group area -->
      <div class="group_area_exam">
        <label>Mô tả</label>
        <textarea></textarea>
      </div>
      <!-- end group area -->
      <!-- button fished -->
      <div class="btn_fished">
        <button type="submit">
          {{ this.$route.query.action == "edit" ? "Cập nhật" : "Hoàn tất" }}
        </button>
      </div>
      <!-- end button fished -->
    </form>
  </div>
</template>
<script>
import { computed, ref } from "vue";
import store from "@/store";
import { useToast } from "vue-toastification";
export default {
  name: "HomeView",
  created() {
    const check = setInterval(() => {
      if (store.state.course.course != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          this.$route.query.action == "edit"
            ? ((store.state.course.data_post.id = Number(this.$route.query.id)),
              (store.state.course.data_post.name = store.getters[
                "course/getbyid"
              ](Number(this.$route.query.id))[0].name),
              (store.state.course.data_post.price = store.getters[
                "course/getbyid"
              ](Number(this.$route.query.id))[0].price),
              (store.state.course.data_post.id_cer = store.getters[
                "course/getbyid"
              ](Number(this.$route.query.id))[0].id_cer),
              (store.state.course.data_post.id_lang = store.getters[
                "course/getbyid"
              ](Number(this.$route.query.id))[0].id_lang))
            : null;
        }
      }
    }, 200);
  },
  setup() {
    const toast = useToast();
    store.dispatch("course/get_all");
    store.dispatch("languages/get_all");
    store.dispatch("certificate/get_all");
    const err = computed(() => {
      return store.state.course.err;
    });
    const languages = computed(() => {
      return store.state.languages.languages;
    });
    const certificate = computed(() => {
      return store.state.certificate.certificate;
    });
    const data = computed(() => {
      return store.state.course.data_post;
    });
    const reset_data = () => {
      data.value.id_cer = null;
      data.value.id_lang = null;
      data.value.name = null;
      data.value.price = null;
    };
    const status_dropdown_languages = ref(null);
    const status_dropdown_certicate = ref(null);
    const onsubmit = () => {
      data.value.id_cer == null
        ? (err.value.certificate = "Không bỏ trống chứng chỉ")
        : (err.value.certificate = null);
      data.value.id_lang == null
        ? (err.value.languages = "Không bỏ trống ngôn ngữ")
        : (err.value.languages = null);
      data.value.name == null || data.value.name == ""
        ? (err.value.name = "Không bỏ trống tên khóa học")
        : (err.value.name = null);
      data.value.price == null || data.value.price == 0
        ? (err.value.price = "Không bỏ trống giá")
        : data.value.price < 0
        ? (err.value.price = "Giá lớn hơn 0")
        : (err.value.price = null);
      err.value.price == null &&
      err.value.certificate == null &&
      err.value.languages == null &&
      err.value.name == null
        ? data.value.id == null
          ? (store.dispatch("course/create"),
            toast.success("Tạo thành công"),
            reset_data())
          : (store.dispatch("course/update"),
            toast.success("Cập nhật thành công"))
        : data.value.id == null
        ? toast.error("Cập nhật thất bại")
        : toast.error("Cập nhật thất bại");
    };
    return {
      onsubmit,
      err,
      data,
      languages,
      certificate,
      status_dropdown_languages,
      status_dropdown_certicate,
    };
  },
};
</script>
<style scoped>
.err {
  position: absolute;
  top: calc(100% + 10px);
  font-size: 14px;
  color: red;
}
.group_add_exam > div {
  position: relative;
}
/* button fished */
.btn_fished > button {
  border-radius: 10px;
  background: rgb(187, 4, 4);
  display: flex;
  width: 172px;
  height: 44px;
  padding: 5px 11px;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 10px;
  color: white;
  border: none;
}
.btn_fished {
  padding: 30px;
  display: flex;
  justify-content: center;
  align-items: center;
}
/* and button fished */
/* common  */
input {
  color: var(--color_main) !important;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  height: var(--size-field_height);
  border-radius: 10px;
  border: 1px solid var(--color_main);
}
/* text area */
.group_area_exam {
  align-items: start;
  display: flex;
  flex-direction: column;
}
textarea {
  resize: none;
  outline: none;
  width: 671px;
  border: 1px solid var(--color_main);
  min-height: 148px;
  border-radius: 10px;
}
/* text area end */
.group_check_exam,
.group_area_exam {
  text-align: start;
}
.group_check {
  padding: 20px 0;
}
/* check radio */
.radio {
  display: none;
}
.icon_check {
  position: absolute;
  top: 50%;
  left: 50%;
  display: none;
  transform: translate(-50%, -50%);
}
.radio:checked ~ .btn > .icon_check {
  display: block;
}
.btn {
  position: relative;
  width: 25px;
  display: block;
  height: 25px;
  border-radius: 5px;
  border: 1px solid var(--color_main);
}
.group_check {
  display: flex;
  gap: 30px;
}
.group_check > label {
  display: flex;
  gap: 14px;
  align-items: center;
}
/* end radio */
/* set input  */
label {
  margin-bottom: 9px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
/* end set input */
.name_add_exam,
.language_add_exam,
.price_add_exam,
.ducation_add_exam {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
.from_add_exam {
  border-radius: 10px;
  background: white;
  padding-top: 50px;
  padding-left: 50px;
  padding-right: 50px;
}

/* group name and language */
.group_add_exam {
  display: grid;
  grid-template-columns: 330px 330px;
  gap: 20px;
  margin-bottom: 40px;
}
.group_add_exam > div {
  display: flex !important;
}
.group_add_exam > div > input,
.group_add_exam > div > div {
  width: 100% !important;
}
/* end group name and language */
h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  width: 299px;
  position: relative;
}
.dropdown_input {
  position: relative;
  height: var(--size-field_height);
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-size: 14px;
}
.dropdown_menu {
  transition: 0.1s;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  pointer-events: none;
  z-index: 1;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
/* end drop down  */
</style>
