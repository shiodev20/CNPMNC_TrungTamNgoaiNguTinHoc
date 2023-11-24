<template>
  <div class="add_exam">
    <h1>
      {{ this.$route.query.action == "edit" ? "Cập nhật" : "Thêm" }} kỳ thi
    </h1>
    <form @submit.prevent="onsubmit" class="from_add_exam">
      <div class="group_add_exam">
        <div class="name_add_exam">
          <label>Tên kỳ thi</label>
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
              <span v-if="data.language_id" class="text">
                {{
                  $store.getters["languages/getbyid"](data.language_id)[0].name
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
                  (data.language_id = item.id),
                    (status_dropdown_languages = false)
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
              <span v-if="data.certificate_id" class="text">
                {{
                  $store.getters["certificate/getbyid"](data.certificate_id)[0]
                    .name
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
                  (data.certificate_id = item.id),
                    (status_dropdown_certicate = false)
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

      <!-- group check -->
      <div class="group_check_exam">
        <label>Hình thức thi</label>
        <div class="group_check">
          <label v-for="item in content_exam" :key="item.id">
            <input
              class="radio"
              type="checkbox"
              :id="'item' + item.id"
              :value="item.id"
              name="from_exam"
            />
            <span class="btn"
              ><font-awesome-icon class="icon_check" :icon="['fas', 'check']"
            /></span>
            <span>{{ item.name }}</span>
          </label>
          <span class="err" v-if="err.exam_type">{{ err.exam_type }}</span>
        </div>
      </div>
      <!-- end group check -->

      <!-- group area -->
      <div class="group_area_exam">
        <label>Mô tả</label>
        <textarea v-model="data.decription"></textarea>
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
import { useToast } from "vue-toastification";
import store from "@/store";
export default {
  name: "HomeView",
  created() {
    const check = setInterval(() => {
      if (store.state.exam.exam != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          this.$route.query.action == "edit"
            ? ((store.state.exam.data_post.id = Number(this.$route.query.id)),
              (store.state.exam.data_post.name = store.getters["exam/getbyid"](
                Number(this.$route.query.id)
              ).name),
              (store.state.exam.data_post.price = store.getters["exam/getbyid"](
                Number(this.$route.query.id)
              ).price),
              (store.state.exam.data_post.certificate_id = store.getters[
                "exam/getbyid"
              ](Number(this.$route.query.id)).certificate_id),
              (store.state.exam.data_post.language_id = store.getters[
                "exam/getbyid"
              ](Number(this.$route.query.id)).language_id),
              (store.state.exam.data_post.exam_type_id = store.getters[
                "exam/getbyid"
              ](Number(this.$route.query.id)).exam_type_id))
            : null;
          // cập nhật check
          this.$route.query.action == "edit"
            ? store.state.exam.data_post.exam_type_id.length > 0
              ? store.state.exam.data_post.exam_type_id.forEach((e) => {
                  console.log(e);
                  document.getElementById("item" + e).checked = true;
                })
              : null
            : null;
        }
      }
    }, 200);
  },
  setup() {
    const toast = useToast();
    store.dispatch("exam/get_all");
    store.dispatch("languages/get_all");
    store.dispatch("certificate/get_all");
    store.dispatch("content_exame/get_all");
    const err = computed(() => {
      return store.state.exam.err;
    });
    const languages = computed(() => {
      return store.state.languages.languages;
    });
    const certificate = computed(() => {
      return store.state.certificate.certificate;
    });
    const data = computed(() => {
      return store.state.exam.data_post;
    });
    const status_dropdown_languages = ref(null);
    const status_dropdown_certicate = ref(null);

    const resest_date = () => {
      data.value.language_id = null;
      data.value.language_id = null;
      data.value.name = null;
      data.value.price = null;
      content_exam.value.forEach((e) => {
        document.getElementById("item" + e.id).checked = false;
      });
    };

    const onsubmit = () => {
      data.value.certificate_id == null
        ? (err.value.certificate = "Không bỏ trống chứng chỉ")
        : (err.value.certificate = null);
      data.value.language_id == null
        ? (err.value.languages = "Không bỏ trống ngôn ngữ")
        : (err.value.languages = null);
      data.value.name == "" || data.value.name == null
        ? (err.value.name = "Không bỏ trống tên khóa học")
        : (err.value.name = null);
      data.value.price == 0 || data.value.name == null
        ? (err.value.price = "Không bỏ trống giá")
        : data.value.price < 0
        ? (err.value.price = "Giá lớn hơn 0")
        : (err.value.price = null);
      var check = false;
      content_exam.value.forEach((e) => {
        if (document.getElementById("item" + e.id).checked == true) {
          check = true;
        }
      });
      check == false
        ? (err.value.exam_type = "Không bỏ trống hình thức")
        : (err.value.exam_type = null);
      data.value.exam_type_id = [];
      document.querySelectorAll(".radio").forEach((e) => {
        e.checked == true
          ? data.value.exam_type_id.push(Number(e.value))
          : null;
      });
      err.value.price == null &&
      err.value.certificate == null &&
      err.value.languages == null &&
      err.value.name == null &&
      err.value.exam_type == null
        ? data.value.id == null
          ? (store.dispatch("exam/create"),
            toast.success("Tạo thành công"),
            resest_date())
          : (store.dispatch("exam/update"),
            toast.success("Cập nhật thành công"),
            (store.state.course.data_post.id = null))
        : data.value.id == null
        ? toast.error("Tạo thất bại")
        : toast.error("Cập nhật thất bại");
    };
    const content_exam = computed(() => {
      return store.state.content_exame.content_exame;
    });
    return {
      onsubmit,
      data,
      err,
      languages,
      certificate,
      status_dropdown_languages,
      status_dropdown_certicate,
      content_exam,
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
  padding-top: 20px;
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
  margin: 20px 0;
  position: relative;
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
  flex-wrap: wrap;
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
  width: 671px;
}

/* group name and language */
.group_add_exam {
  display: grid;
  grid-template-columns: 330px 330px;
  gap: 20px;
  margin-bottom: 40px;
}
.group_add_exam > div {
  position: relative;
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
  font-weight: 600;
  font-size: 16px;
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
