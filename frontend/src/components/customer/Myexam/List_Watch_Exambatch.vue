<template>
  <div class="parent">
    <h3 class="title">Đợt thi của tôi</h3>
    <div v-if="exambatch" class="parent01">
      <div v-if="exambatch.length > 0" class="boss-card">
        <div
          :id="'item' + item.id"
          v-for="item in exambatch_for_exam"
          :key="item.id"
          class="card"
        >
          <h3>{{ exambatch_id(item.exam_session_id).name }}</h3>
          <div class="card-content">
            <h3
              v-if="$store.state.exam.exam && content_exame"
              class="card-item"
            >
              <span
                v-for="itemx in exambatch_id(item.exam_session_id).exam_data"
                :key="itemx"
                >{{ content_exam_id(itemx.exam_type).name + " " }}</span
              >
            </h3>
            <p
              :style="
                exambatch_id(item.exam_session_id).status == true
                  ? { color: 'rgba(127, 187, 0, 1)' }
                  : null
              "
              class="price"
            >
              {{
                exambatch_id(item.exam_session_id).status == true
                  ? "Hiệu lực"
                  : "Vô hiệu"
              }}
            </p>
          </div>
          <div v-if="cusexam" class="parent_btn">
            <button class="btn">hóa đơn</button>
            <button
              v-if="item.status_pay"
              class="btn"
              @click="
                this.$router.push({
                  name: 'list-myexam',
                  query: { action: 'detail', id: item.exam_session_id },
                })
              "
            >
              Xem lịch thi
            </button>
            <button v-if="!item.status_pay" style="opacity: 0.5" class="btn">
              Chưa thanh toán
            </button>
          </div>
        </div>

        <!-- pagnigation -->
        <!-- end pagnigation -->
      </div>
      <div class="images" v-else>
        <img src="../../Imagess/notfound.png" alt="" />
      </div>
      <div v-if="exambatch.length > 0" class="group_page">
        <div
          @click="chage(this_page - 1)"
          v-if="this_page > 1 && count_page > 0"
          class="pre"
        >
          <font-awesome-icon :icon="['fas', 'arrow-left']" />
        </div>
        <div
          :class="this_page == item ? 'show_item_page' : null"
          @click="chage(item)"
          v-for="item in count_page"
          :key="item"
          class="item_page"
        >
          {{ item }}
        </div>
        <div
          @click="chage(this_page + 1)"
          v-if="this_page < count_page"
          class="next"
        >
          <font-awesome-icon :icon="['fas', 'arrow-right']" />
        </div>
      </div>
    </div>
  </div>
  <basemodal v-if="$store.state.test.status_modal == true"></basemodal>
</template>
<script>
import { computed, reactive, ref, watch } from "vue";
import store from "@/store";
import modal from "@/components/common/Modal_Requid_loginV.vue";
export default {
  components: {
    basemodal: modal,
  },
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.exambatch_for_exam) {
      const div = document.getElementById(
        "item" + this.exambatch_for_exam[item].id
      );
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  setup() {
    store.dispatch("exam/get_all");
    store.dispatch("exambatch/get_all");
    store.dispatch("content_exame/get_all");
    store.dispatch("cus_exam/get_all");
    store.state.acount.authencation = store.getters["acount/check_token"]();
    const content_exame = computed(() => {
      return store.state.content_exame.content_exame;
    });
    const content_exam_id = (value) => {
      return store.getters["content_exame/getbyid"](value)[0];
    };
    const this_page = ref(1);
    const limit_page = ref(6);

    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in exambatch_for_exam.value) {
        const div = document.getElementById(
          "item" + exambatch_for_exam.value[item].id
        );
        if (item >= page_start && item <= page_end) {
          div.classList.contains("hide") ? div.classList.remove("hide") : null;
          div.classList.add("show");
        } else {
          div.classList.contains("show") ? div.classList.remove("show") : null;
          div.classList.add("hide");
        }
      }
    });

    const count_page = computed(() => {
      if (exambatch_for_exam.value == null) {
        return 0;
      }
      return Math.ceil(exambatch_for_exam.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
      console.log(this_page.value);
    };

    const exambatch_for_exam = computed(() => {
      if (store.state.cus_exam.cus_exam == null) {
        return [];
      }
      return store.getters["cus_exam/getbyidcus"](
        store.state.acount.authencation.id
      );
    });
    const registerexam = (idexam) => {
      if (!localStorage.getItem("token")) {
        store.state.test.status_modal = true;
      } else {
        const data = reactive({
          date_create: new Date().getTime(),
          customer_id: Number(store.state.acount.authencation.id),
          exam_session_id: Number(idexam),
          status_pay: false,
          result: null,
        });
        store.dispatch("cus_exam/add_cus", data);
      }
    };
    const cusexam_id = (cus, idclass) => {
      if (cus != null) {
        return store.getters["cus_exam/getbyid"](cus.id, idclass);
      }
      return null;
    };
    const exambatch = computed(() => {
      return store.state.exambatch.exambatch;
    });
    const exambatch_id = (value) => {
      return store.getters["exambatch/getbyid"](value)[0];
    };
    const cusexam = computed(() => {
      return store.state.cus_exam.cus_exam;
    });
    return {
      exambatch_for_exam,
      run_app,
      count_page,
      this_page,
      limit_page,
      chage,
      content_exame,
      content_exam_id,
      registerexam,
      cusexam_id,
      cusexam,
      exambatch_id,
      exambatch,
    };
  },
};
</script>
<style scoped>
.parent_btn {
  margin-top: 10px;
  text-align: end;
  display: flex;
  justify-content: space-between;
}
.info_parent {
  text-align: start;
  padding: 20px;
  padding-bottom: 0;
  font-size: 18px;
}
.info_parent > p {
  margin-bottom: 5px;
}
.parent_btn {
  text-align: end;
}
.show_item_page {
  background: var(--color_table);
  color: white !important;
}
.hide {
  display: none;
}
.show {
  display: table-row !important;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
  gap: 10px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
.title {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
  margin-bottom: 17px;
}
.setting {
  padding: 20px;
  padding-top: 0px;
  text-align: end;
}
.Header {
  color: #071952;
  text-align: right;
  font-family: Roboto;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  margin-right: 30px;
}
.content-header {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

.card-item {
  color: #000;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.price {
  color: rgb(197, 3, 3);
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

p {
  margin: 0;
}

.btn {
  border: 2px solid #071952;
  color: #071952;
  padding: 10px 30px;
  border-radius: 10px;
  cursor: pointer;
}
.parent {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
.boss-card {
  background-color: white;
  display: grid;
  grid-template-columns: repeat(3, 300px);
  gap: 25px;
  border-radius: 10px;
  padding: 25px;
  padding-bottom: 60px;
  position: relative;
}
.parent01 {
  background-color: white;
  border-radius: 20px;
  min-height: 600px;
  min-width: 1000px;
  position: relative;
}
.image-card {
  width: 100%;
}

.card {
  position: relative;
  border-radius: 10px;
  background: #f7f7f7;
  overflow: hidden;
  padding: 10px 10px;
  text-align: start;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.card-content {
  padding: 10px 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
h3 {
  margin: 0;
}
</style>
